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
    public class BugCommentController : ControllerBase
    {
        private readonly IBugService _bugService;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IBugCommentService _bugCommentService;

        public BugCommentController(IBugService bugService,
                             IHubContext<NotificationHub> hubContext,
                             IBugCommentService bugCommentService)
        {
            _bugService = bugService;
            _hubContext = hubContext;
            _bugCommentService = bugCommentService;
        }

        [HttpPost("Comment")]
        [CustomExceptionFilter]
        [Authorize(Roles = "Admin,Tester,Dev")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<BugCommentResponse>> AddComment([FromBody] BugCommentRequest bugCommentReq)
        {
            string? email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null)
            {
                throw new Exception("Not logged in");
            }
            var result = await _bugCommentService.AddComment(bugCommentReq, email);

            if (result == null)
            {
                throw new Exception("error occured while submmitting bug");
            }
            return Created("", result);
        }


        [HttpGet("GetComments")]
        [CustomExceptionFilter]
        [Authorize(Roles = "Admin,Tester,Dev")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<string>>> GetAllComments([FromQuery] long bugId)
        {
            var result = await _bugCommentService.GetAllCommentsForBug(bugId);
            return Ok(result);

        }

        [HttpDelete("Delete")]
        [CustomExceptionFilter]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<string>> DeleteComment([FromQuery] long commentId)
        {
            var SubmitterEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            if (SubmitterEmail == null)
            {
                throw new Exception("Not logged In");
            }
            var result = await _bugCommentService.DeleteComments(commentId,SubmitterEmail);
            return Ok(result);
        }


    }

       

        
    
}