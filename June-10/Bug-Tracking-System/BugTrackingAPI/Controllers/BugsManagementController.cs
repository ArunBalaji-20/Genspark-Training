using System.Security.Claims;
using Asp.Versioning;
using BugTrackingAPI.Interfaces;
using BugTrackingAPI.Misc;
using BugTrackingAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BugTrackingAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BugsManagementController : ControllerBase
    {
        private readonly IBugManagementService _BugManagementService;

        public BugsManagementController(IBugManagementService bugAssignmentService)
        {
            _BugManagementService = bugAssignmentService;
        }

        [HttpPost("Assign")]
        [Authorize(Roles = "Admin")]
        [CustomExceptionFilter]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<BugAssignmentResponse>> AssignBugsToDev([FromBody] BugAssignmentRequest bugAssgnDto)
        {
            var SubmitterEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            if (SubmitterEmail == null)
            {
                throw new Exception("Not logged In");
            }

            var result = await _BugManagementService.AssignBugToDevs(bugAssgnDto.BugId, bugAssgnDto.DevId, SubmitterEmail);
            if (result == null)
            {
                throw new Exception("error occured while submmitting bug");
            }
            return Created("",result);
        }

        [HttpPatch("Resolve")]
        [Authorize(Roles = "Dev")]
        [CustomExceptionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<BugAssignmentResponse>> ResolveBugs([FromQuery] long BugId)
        {
            var SubmitterEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            if (SubmitterEmail == null)
            {
                throw new Exception("Not logged In");
            }

            var result = await _BugManagementService.ResolveBugs(BugId, SubmitterEmail);
            if (result == null)
            {
                throw new Exception("error occured while submmitting bug");
            }
            return Ok(result);
        }
    }
}