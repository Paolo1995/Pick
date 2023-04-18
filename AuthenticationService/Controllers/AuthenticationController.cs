using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] string request)
        {
            // ...
            return null;
        }
    }
}


//https://identityserver4.readthedocs.io/en/latest/topics/startup.html