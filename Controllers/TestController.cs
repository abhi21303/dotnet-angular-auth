using Microsoft.AspNetCore.Mvc;

namespace DotNetWebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("hello")]
        public IActionResult GetHello()
        {
            return Ok(new { message = "Hello from API!" });
        }
    }
}
