using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading.Tasks;

namespace mvc_client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Secret()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var idToken = await HttpContext.GetTokenAsync("id_token");
            // not implemented
            //var refreshToken = await HttpContext.GetTokenAsync("refresh_token");

            // just for visibility purpose
            var _accessToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
            var _idToken = new JwtSecurityTokenHandler().ReadJwtToken(idToken);
            // not implemented
            //var _refreshToken = new JwtSecurityTokenHandler().ReadJwtToken(refreshToken);

            var result = await GetSecret(accessToken);

            return View();
        }

        public IActionResult Logout()
        {
            return SignOut("Cookie", "oidc");
        }

        public async Task<string> GetSecret(string accessToken)
        {
            // retrieve secret data from ApiOne
            var apiClient = _httpClientFactory.CreateClient();

            apiClient.SetBearerToken(accessToken);

            var response = await apiClient.GetAsync("https://localhost:41000/secret");

            var content = await response.Content.ReadAsStringAsync();

            return content;
        }
    }
}
