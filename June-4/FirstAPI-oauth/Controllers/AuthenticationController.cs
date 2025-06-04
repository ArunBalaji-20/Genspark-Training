
using System.Security.Claims;
using FirstAPI.Interfaces;
using FirstAPI.Models.DTOs.DoctorSpecialities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;


namespace FirstAPI.Controllers
{


    [ApiController]
    [Route("/api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly Interfaces.IAuthenticationService _authenticationService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(Interfaces.IAuthenticationService authenticationService, ILogger<AuthenticationController> logger)
        {
            _authenticationService = authenticationService;
            _logger = logger;
        }
        [HttpPost]
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


        [HttpGet("Oauth")]
        public IActionResult ExternalLogin([FromQuery] string provider)
        {
            var redirectUrl = Url.Action("OAuthCallback", "Authentication");
            var properties = new AuthenticationProperties
            {
                RedirectUri = redirectUrl
            };
            return Challenge(properties, provider); // provider = "Google"
        }

        [HttpGet("oauth/callback")]
        public async Task<IActionResult> OAuthCallback()
        {
            // // var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            // var result = HttpContext.User;
            // if (!result.Succeeded)
            // {
            //     return Unauthorized("OAuth login failed");
            // }

            // var claims = result.Principal.Claims.ToList();
            // var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            // var name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            // var id = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            // Console.WriteLine("OAuth User Info:");
            // Console.WriteLine($"ID: {id}");
            // Console.WriteLine($"Name: {name}");
            // Console.WriteLine($"Email: {email}");

            // return Ok(new { message = "OAuth login successful", id, name, email });

            var user = HttpContext.User;
            string email = user.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
            string name = user.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
            // Log or store the user information
            // ...
            return Redirect("/");
        }
    

        // [HttpGet("oauth/callback")]
        // public  Task<IActionResult> OAuthCallback()
        // {
        //     throw new NotImplementedException();
        //     // var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        //     // if (!result.Succeeded)
        //     //     return Unauthorized("OAuth login failed");

        //     // var email = result.Principal.FindFirstValue(ClaimTypes.Email);

        //     // // Check if user exists in DB
        //     // // var user = await _userService.GetUserByEmailAsync(email);
        //     // if (user == null)
        //     // {
        //     //     // Optionally create user or return error
        //     //     return BadRequest("No user found");
        //     // }

        //     // var role = user.Role;

        //     // // Create your JWT
        //     // // var token = _jwtService.GenerateToken(email, role);

        //     // return Ok(new { token });
        // }
    }
}