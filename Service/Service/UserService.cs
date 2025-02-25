using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObject;
using BusinessObject.Enum;
using BusinessObject.Model;
using BusinessObject.RequestDTO;
using BusinessObject.ResponseDTO;
using Repository;
using static BusinessObject.RequestDTO.RequestDTO;
using static BusinessObject.ResponseDTO.ResponseDTO;

namespace Service.Service
{
    public interface IUserService
    {
        Task<ResponseDTO> GetListUsersAsync();
        Task<ResponseDTO> Login(LoginRequestDTO request);
    }
    public class UserService : GenericRepository<User>, IUserService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jWTService;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IJWTService jWTService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jWTService = jWTService;
        }

        public async Task<ResponseDTO> GetListUsersAsync()
        {
            try
            {
                var users = await _unitOfWork.UserRepository.GetAllAsync();

                if (users == null || !users.Any())
                {
                    return new ResponseDTO(Const.SUCCESS_CREATE_CODE, "Empty List");
                }

                // Chỉ lấy UserId, UserName, Password
                var result = _mapper.Map<List<UserListDTO>>(users);

                return new ResponseDTO(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);
            }
            catch (Exception e)
            {
                return new ResponseDTO(Const.ERROR_EXCEPTION, e.Message);
            }
        }


        public async Task<ResponseDTO> Login(LoginRequestDTO request)
        {
            try
            {
                var account = _unitOfWork.UserRepository.GetAll()
                             .FirstOrDefault(x => x.Username!.ToLower() == request.Username.ToLower()
                             && x.Password == request.Password);

                if (account == null)
                {             
                    return new ResponseDTO(Const.FAIL_READ_CODE, "Invalid username or password.");
                }

                if (account.Password != request.Password)  // Nếu dùng hash, cần giải mã password
                {
                    return new ResponseDTO(Const.FAIL_READ_CODE, "Invalid username or password.");
                }

                var jwt = _jWTService.GenerateToken(account);
                return new ResponseDTO(Const.SUCCESS_READ_CODE, "Login successful", jwt);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Exception: {ex.Message}");
                return new ResponseDTO(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        
    }
}
