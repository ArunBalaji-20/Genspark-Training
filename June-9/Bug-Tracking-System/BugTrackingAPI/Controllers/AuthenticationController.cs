
using Asp.Versioning;
using BugTrackingAPI.Interfaces;
using BugTrackingAPI.Models;
using BugTrackingAPI.Models.DTO;
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
        public async Task<ActionResult<UserLoginResponse>> RegisterUser(UserRegisterRequest RegRequest)
        {
            try
            {
                var result = await _authenticationService.Register(RegRequest);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
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
        public async Task<ActionResult<TokenResponse>> GetNewAccessTokenWithRefresh([FromBody] RefreshRequestModel request)
        {
            try
            {
                var tokens = await _authenticationService.RefreshTokenAsync(request.RefreshToken);
                return Ok(tokens);
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
        }

    }
}