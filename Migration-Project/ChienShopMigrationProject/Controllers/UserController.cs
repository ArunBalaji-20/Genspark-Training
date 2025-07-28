using ChienShopMigrationProject.Interfaces;
using ChienVHShopOnline.Models;
using ChienVHShopOnline.Models.Dtos;
using ChienVHShopOnline.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChienShopMigrationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> RegisterUser(RegisterUserDto user)
        {
            try
            {
                var result = await _userService.Register(user);
                return result;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login(RegisterUserDto user)
        {
            try
            {
                var result = await _userService.Login(user);
                return result;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
