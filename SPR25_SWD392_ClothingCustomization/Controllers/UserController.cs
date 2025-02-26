using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessObject;
using BusinessObject.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Service.Service;
using static BusinessObject.RequestDTO.RequestDTO;

namespace SPR25_SWD392_ClothingCustomization.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        public UserController(IConfiguration config,IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userService.Authenticate(request.UserName, request.Password);

            if (user == null || user.Result == null)
                return Unauthorized();

            var token = GenerateJSONWebToken(user.Result);

            return Ok(token);
        }

        private string GenerateJSONWebToken(User userAccount)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"]
                    , _config["Jwt:Audience"]
                    , new Claim[]
                    {
                new(ClaimTypes.Name, userAccount.Username),
                //new(ClaimTypes.Email, systemUserAccount.Email),
                new(ClaimTypes.Role, userAccount.RoleId.ToString()),
                    },
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

        public sealed record LoginRequest(string UserName, string Password);
        [HttpGet("userList")]
        //[Authorize(Roles = "1,2")]
        public async Task<IActionResult> GetListUser()
        {
            var result = await _userService.GetListUsersAsync();
            // Kiểm tra kết quả và trả về phản hồi phù hợp
            if (result.Status != Const.SUCCESS_READ_CODE)
            {
                return BadRequest(result); // Trả về mã lỗi 400 với thông báo lỗi từ ResponseDTO
            }
            return Ok(result);
        }
    }
}
