
using System.Security.Claims;
using Asp.Versioning;
using BugTrackingAPI.Interfaces;
using BugTrackingAPI.Misc;
using BugTrackingAPI.Models;
using BugTrackingAPI.Models.DTO;
using BugTrackingAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;


namespace BugTrackingAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    // [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ITokenService _tokenService;

        public AuthenticationController(IAuthenticationService authenticationService,
                                        ITokenService tokenService)
        {
            _authenticationService = authenticationService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserLoginResponse>> RegisterUser(UserRegisterRequest RegRequest)
        {
            try
            {
                var result = await _authenticationService.Register(RegRequest);
                return Created("", result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserLoginResponse>> UserLogin(UserLoginRequest loginRequest)
        {
            try
            {
                var result = await _authenticationService.Login(loginRequest);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
        }

        [HttpPost("Refresh")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TokenResponse>> GetNewAccessTokenWithRefresh([FromBody] RefreshRequestModel request)
        {
            try
            {
                var tokens = await _authenticationService.RefreshTokenAsync(request.RefreshToken);
                return Created("",tokens);
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
        }

        [HttpPost("logout")]
        [CustomExceptionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Logout([FromBody] LogOutRequest request)
        {
            // Extract access token from Authorization header
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            string accessToken = null;
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                accessToken = authHeader.Substring("Bearer ".Length).Trim();
            }
            else
            {
                return Unauthorized("Access token is missing.");
            }

            await _authenticationService.LogOut(request, accessToken);

            return Ok(new { message = "Logged out successfully" });
        }



        [HttpGet("me")]
        public ActionResult<string> GetLoggedInUser()
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                var Role = User.FindFirst(ClaimTypes.Role)?.Value;
                var data = new
                {
                    EmailId = email,
                    Role = Role
                };

                return Ok(data);

            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
        }



    }
}