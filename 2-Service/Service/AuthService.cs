using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.RequestDTO;
using static BusinessObject.RequestDTO.RequestDTO;

namespace _2_Service.Service
{
    public interface IAuthService
    {
        Task<TokenResponse> ExchangeCodeForTokens(string code);
    }


    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
        }

        public async Task<TokenResponse> ExchangeCodeForTokens(string code)
        {
            var values = new Dictionary<string, string>
        {
            { "client_id", _configuration["Authentication:Google:ClientId"] },
            { "client_secret", _configuration["Authentication:Google:ClientSecret"] },
            { "code", code },
            { "redirect_uri", "https://localhost:3000" },
            { "grant_type", "authorization_code" }
        };

            var content = new FormUrlEncodedContent(values);
            var response = await _httpClient.PostAsync("https://oauth2.googleapis.com/token", content);

            if (!response.IsSuccessStatusCode)
                return null;

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TokenResponse>(responseString);
        }
    }
}
