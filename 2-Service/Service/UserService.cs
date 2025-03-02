﻿using _3_Repository.IRepository;
using _3_Repository.Repository;
using Azure.Core;
using BusinessObject;
using BusinessObject.Model;
using BusinessObject.ResponseDTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessObject.RequestDTO.RequestDTO;
using static BusinessObject.ResponseDTO.ResponseDTO;

namespace _2_Service.Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task AddUser(UserRegisterDTO userDto);
        Task<ResponseDTO> Login(LoginRequestDTO userDto);
        Task UpdateUser(int id, UserDTO userDto);
        Task DeleteUser(int id);


    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IJWTService _jWTService;
        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IJWTService jWTService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _jWTService = jWTService;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task AddUser(UserRegisterDTO userDto)
        {
            var role = await _roleRepository.GetIdByNameAsync(userDto.RoleName);
            if (role == null) throw new Exception("Invalid role.");

            var user = new User
            {
                Username = userDto.Username,
                Password = userDto.Password, // Will be hashed in repository
                FullName = userDto.FullName,
                Email = userDto.Email,
                Gender = userDto.Gender,
                DateOfBirth = userDto.DateOfBirth,
                Address = userDto.Address,
                Phone = userDto.Phone,
                Avatar = userDto.Avatar,
                IsDeleted = false,
                RoleId = role.RoleId
            };
            await _userRepository.AddAsync(user);
        }

        public async Task<ResponseDTO> Login(LoginRequestDTO userDto)
        {
            try
            {
                var account = await _userRepository.GetByUsernameAsync(userDto.Username);

                if (account == null)
                {
                    return new ResponseDTO(Const.FAIL_READ_CODE, "Invalid username or password.");
                }

                if (account.Password != userDto.Password)  // Nếu dùng hash, cần giải mã password
                {
                    return new ResponseDTO(Const.FAIL_READ_CODE, "Invalid username or password.");
                }

                var jwt = _jWTService.GenerateToken(account);

                //var loginResponse = new ResponseDTO.LoginResponse
                //{
                //    UserId = account.UserId,
                //    UserName = account.Username,
                //    Password = account.Password,
                //    Phone = account.Phone,
                //    FullName = account.FullName,
                //    IsDeleted = account.IsDeleted,
                //    RoleName = account.Role.RoleName,
                //};
                //return new ResponseDTO(Const.SUCCESS_READ_CODE, "Login successful", new
                //{
                //    Token = jwt,
                //    UserData = loginResponse
                //});

                return new ResponseDTO(Const.SUCCESS_READ_CODE, "Login successful", jwt);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Exception: {ex.Message}");
                return new ResponseDTO(Const.ERROR_EXCEPTION, ex.Message);
            }
        }


        public async Task UpdateUser(int id, UserDTO userDto)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null) throw new Exception("User not found.");

            existingUser.FullName = userDto.FullName;
            existingUser.Email = userDto.Email;
            existingUser.Gender = userDto.Gender;
            existingUser.DateOfBirth = userDto.DateOfBirth;
            existingUser.Address = userDto.Address;
            existingUser.Phone = userDto.Phone;
            existingUser.Avatar = userDto.Avatar;

            await _userRepository.UpdateAsync(existingUser);
        }

        public async Task DeleteUser(int id)
        {
            await _userRepository.SoftDeleteAsync(id);
        }

    }
}
