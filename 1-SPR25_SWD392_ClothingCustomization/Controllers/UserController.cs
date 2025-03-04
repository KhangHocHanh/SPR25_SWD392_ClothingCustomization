using _2_Service.Service;
using BusinessObject;
using BusinessObject.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BusinessObject.RequestDTO.RequestDTO;

namespace _1_SPR25_SWD392_ClothingCustomization.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet("{id}")]
        //  [Authorize]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromBody] UserRegisterDTO userDto)
        {
            try
            {
                await _userService.AddUser(userDto);
                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO userDto)
        {
            try
            {
                var response = await _userService.Login(userDto);

                if (response.Status == Const.FAIL_READ_CODE)
                {
                    return Unauthorized(response.Message);
                }

                if (response.Data == null) // Check if the token is missing
                {
                    return StatusCode(500, new { message = "Login failed due to internal error." });
                }
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                // return BadRequest(ex.Message);
                return StatusCode(500, new { message = "Internal Server Error", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UserDTO userDto)
        {
            await _userService.UpdateUser(id, userDto);
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }


        [HttpPost("GoogleLogin")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            try
            {
                string jwtToken = await _userService.GoogleLoginAsync(request.IdToken);
                return Ok(new { token = jwtToken });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}
