using Microsoft.AspNetCore.Mvc;

namespace demoapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class sampleController : ControllerBase
    {

        public sampleController()
        {
        }

        [HttpGet("welcome")]
        public  ActionResult<string> demoapi()
        {
            return Ok("Welcome to demoapp");
        }


    }
}
