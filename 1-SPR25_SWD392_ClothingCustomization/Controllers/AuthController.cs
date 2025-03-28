using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using _2_Service.Service;
using static BusinessObject.RequestDTO.RequestDTO;

namespace _1_SPR25_SWD392_ClothingCustomization.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IAuthService authService, IUserService userService)
        {
            _configuration = configuration;
            _authService = authService;
            _userService = userService;
        }

        //[HttpGet("google-login")]
        //public IActionResult GoogleLogin()
        //{
        //    var googleClientId = _configuration["Authentication:Google:ClientId"];
        //    if (string.IsNullOrEmpty(googleClientId))
        //    {
        //        return BadRequest("Google ClientId is missing.");
        //    }

        //    var googleAuthUrl = $"https://accounts.google.com/o/oauth2/v2/auth" +
        //        $"?client_id={googleClientId}" +
        //        $"&scope=openid%20profile%20email" +
        //        $"&response_type=code" +
        //        $"&redirect_uri={Uri.EscapeDataString("https://localhost:7163/api/auth/google-callback")}" +
        //        $"&code_challenge_method=S256" +
        //        $"&code_challenge=YOUR_CODE_CHALLENGE_HERE" +
        //        $"&state=YOUR_RANDOM_STATE";

        //    return Ok(new { url = googleAuthUrl });
        //}

        [HttpGet("google-login")]
        public IActionResult GoogleLogin()
        {
            var state = Guid.NewGuid().ToString(); // Generate unique state
            HttpContext.Session.SetString("OAuthState", state); // Store in session

            var googleAuthUrl = $"https://accounts.google.com/o/oauth2/v2/auth" +
                $"?client_id={_configuration["Authentication:Google:ClientId"]}" +
                $"&scope=openid%20profile%20email" +
                $"&response_type=code" +
                $"&redirect_uri={Uri.EscapeDataString("https://localhost:7163/api/auth/google-callback")}" +
                $"&state={state}" + // Attach state
                $"&code_challenge_method=S256" +
                $"&code_challenge=YOUR_CODE_CHALLENGE_HERE";

            return Ok(new { url = googleAuthUrl });
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            var tokenResponse = await _authService.ExchangeCodeForTokens(request.IdToken);

            if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.IdToken))
            {
                return BadRequest("Failed to authenticate with Google.");
            }
            if (_userService == null)
            {
                return BadRequest("User service is not available.");
            }

            string jwtToken = await _userService.GoogleLoginAsync(tokenResponse.IdToken);
            return Ok(new { token = jwtToken });
        }

        [HttpGet("google-callback")]
        public async Task<IActionResult> GoogleResponse([FromQuery] string state, [FromQuery] string code)
        {
            // ✅ Check if the state matches the one in session
            var storedState = HttpContext.Session.GetString("OAuthState");
            if (storedState == null || state != storedState)
            {
                return BadRequest("OAuth state is invalid or missing.");
            }

            // ✅ Exchange code for tokens using Google API
            var tokenResponse = await _authService.ExchangeCodeForTokens(code);

            if (tokenResponse == null)
            {
                return BadRequest("Failed to get tokens from Google.");
            }

            return Ok(new
            {
                tokenResponse.AccessToken,
                tokenResponse.IdToken
            });
        }

        

    }
}
