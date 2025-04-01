using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ResearchProject.Controllers
{
    public class TokenController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public TokenController(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetToken()
        {
            try
            {
                var directLineSecret = _configuration["DirectLineSecret"];
                var request = new HttpRequestMessage(HttpMethod.Post, "https://directline.botframework.com/v3/directline/tokens/generate");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", directLineSecret);
                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var token = JsonDocument.Parse(content).RootElement.GetProperty("token").GetString();
                    return Ok(new { token });
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Error generating token");
                }               
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
