using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SampleController : ControllerBase
    {
        [HttpGet]
        public string GetGreet()
        {
            return "Hello world";
        }

        [HttpGet("sample2")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]

        public ActionResult GetGreetTwo()
        {
            return Ok("Hello World");
        }
    }
}

//using Microsoft.AspNetCore.Mvc;

//[ApiController]
//[Route("/api/[controller]")]
//public class SampleController : ControllerBase
//{
//    [HttpGet]
//    public string GetGreet()
//    {
//        return "Hello World";
//    }
//}