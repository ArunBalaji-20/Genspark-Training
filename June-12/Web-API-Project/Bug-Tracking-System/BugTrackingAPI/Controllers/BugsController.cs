using System.Security.Claims;
using Asp.Versioning;
using BugTrackingAPI.Hubs;
using BugTrackingAPI.Interfaces;
using BugTrackingAPI.Misc;
using BugTrackingAPI.Models;
using BugTrackingAPI.Models.DTO;
using BugTrackingAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;


namespace BugTrackingAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BugsController : ControllerBase
    {
        private readonly IBugService _bugService;
        private readonly IHubContext<NotificationHub> _hubContext;

        public BugsController(IBugService bugService,
                             IHubContext<NotificationHub> hubContext)
        {
            _bugService = bugService;
            _hubContext = hubContext;
        }

        [HttpGet("ReportedBugs")]
        [Authorize(Roles = "Admin,Tester,Dev")]
        [CustomExceptionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<BugResponse>>> ListAllReportedBugs()
        {
            var result = await _bugService.GetAllBugsList();
            return Ok(result);


        }
        [HttpGet("status")]
        [Authorize(Roles = "Admin,Tester,Dev")]
        [CustomExceptionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
           public async Task<ActionResult<BugResponse>> BugStatus([FromQuery] long BugId)
        {
            var result = await _bugService.GetBugStatus(BugId);
            return Ok(result);


        }

        [HttpPost("Report")]
        [Authorize(Roles = "Admin,Tester")]
        [Consumes("multipart/form-data")]
        [CustomExceptionFilter]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<BugSubmission>> ReportBug([FromForm] BugSubmission bugdto)
        {
            var SubmitterEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            if (SubmitterEmail == null)
            {
                throw new Exception("Not logged In");
            }

            var result = await _bugService.SubmitBug(bugdto, SubmitterEmail);
            if (result == null)
            {
                throw new Exception("error occured while submmitting bug");
            }
            // System.Console.WriteLine("beforre");
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", $"New Bug Reported: {result.BugName}");
            // System.Console.WriteLine("after");
            return Created("", result);

        }
    }
}