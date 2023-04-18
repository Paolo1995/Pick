using Microsoft.AspNetCore.Mvc;

namespace FleetSubscriptionService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        [HttpPost("create-client")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] string request)
        {
            // ...
            return null;
        }

        [HttpGet("example")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetExample()
        {
            return Ok("This is an example response from FleetSubscriptionService.WebAPI.");
        }
    }
}
