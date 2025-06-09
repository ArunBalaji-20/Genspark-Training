using System.Security.Claims;
using Asp.Versioning;
using BugTrackingAPI.Interfaces;
using BugTrackingAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BugTrackingAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BugsAssignmentController : ControllerBase
    {
        private readonly IBugAssignmentService _bugAssignmentService;

        public BugsAssignmentController(IBugAssignmentService bugAssignmentService)
        {
            _bugAssignmentService = bugAssignmentService;
        }

        [HttpPost("Assign")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BugAssignmentResponse>> AssignBugsToDev([FromBody] BugAssignmentRequest bugAssgnDto)
        {
            var SubmitterEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            if (SubmitterEmail == null)
            {
                System.Console.WriteLine("empty....");
                throw new Exception("Not logged In");
            }

            var result = await _bugAssignmentService.AssignBugToDevs(bugAssgnDto.BugId, bugAssgnDto.DevId, SubmitterEmail);
            if (result == null)
            {
                throw new Exception("error occured while submmitting bug");
            }
            return result;
        }
        
        [HttpPost("Resolve")] 
        [Authorize(Roles = "Dev")]
        public async Task<ActionResult<BugAssignmentResponse>> ResolveBugs([FromQuery] long BugId)
        {
            var SubmitterEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            if (SubmitterEmail == null)
            {
                System.Console.WriteLine("empty....");
                throw new Exception("Not logged In");
            }

            var result = await _bugAssignmentService.ResolveBugs(BugId, SubmitterEmail);
            if (result == null)
            {
                throw new Exception("error occured while submmitting bug");
            }
            return result;
        }
    }
}