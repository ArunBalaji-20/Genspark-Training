using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChienShopMigrationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {

        [HttpGet]
        public ActionResult SampleRoute()
        {
            return Ok("Hello World");
        }
    }
}
