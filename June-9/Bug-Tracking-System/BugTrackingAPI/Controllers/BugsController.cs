using System.Security.Claims;
using Asp.Versioning;
using BugTrackingAPI.Hubs;
using BugTrackingAPI.Interfaces;
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
        public async Task<IEnumerable<BugResponse>> ListAllReportedBugs()
        {
            var result = await _bugService.GetAllBugsList();
            return result;

            
        }

        [HttpPost("Report")]
        [Authorize(Roles ="Admin,Tester")]
        [Consumes("multipart/form-data")]
        public async Task<BugSubmission> ReportBug([FromForm] BugSubmission bugdto)
        {
            var SubmitterEmail =User.FindFirst(ClaimTypes.Email)?.Value;

            if (SubmitterEmail == null)
            {
                System.Console.WriteLine("empty....");
                throw new Exception("Not logged In");
            }

            var result = await _bugService.SubmitBug(bugdto, SubmitterEmail);
            if (result == null)
            {
                throw new Exception("error occured while submmitting bug");
            }
            System.Console.WriteLine("beforre");
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", $"New Bug Reported: {result.BugName}");
            System.Console.WriteLine("after");
            return result;

        }
    }
}