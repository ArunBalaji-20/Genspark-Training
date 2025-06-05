using DocNotifyAPI.Interfaces;
using DocNotifyAPI.Models;
using DocNotifyAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;


namespace DocNotifyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly Interfaces.IAuthenticationService _authenticationService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IAuthenticationService authenticationService, ILogger<AuthenticationController> logger)
        {
            _authenticationService = authenticationService;
            _logger = logger;
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
                _logger.LogError(e.Message);
                return Unauthorized(e.Message);
            }
        }
    }
}