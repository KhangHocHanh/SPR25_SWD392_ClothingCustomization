using BusinessObject;
using BusinessObject.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Service.Service;
using static BusinessObject.RequestDTO.RequestDTO;

namespace SPR25_SWD392_ClothingCustomization.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            try
            {
                var response = await _userService.Login(request);

                if (response.Status == Const.FAIL_READ_CODE)
                {
                    return Unauthorized(response.Message);
                }

                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
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
