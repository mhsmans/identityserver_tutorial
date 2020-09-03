using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_one.Controllers
{
    public class SecretController : Controller
    {
        [Route("/secret")]
        [Authorize]
        public string Index()
        {
            
            return "secret message from ApiOne";
        }
    }
}
