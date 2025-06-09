
using Asp.Versioning;
using BugTrackingAPI.Interfaces;
using BugTrackingAPI.Models;
using BugTrackingAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;


namespace BugTrackingAPI.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    // [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        [HttpGet("sample")]
        public ActionResult SampleFunc()
        {
            return Ok("welcome to API V2");
        }

    }
}