using BankingAPP.DTOs;
using BankingAPP.Interfaces;
using BankingAPP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingAPP.Controllers
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

        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUserAccounts()
        {
            var AllAccounts = await _userService.GetAllUsers();
            return Ok(AllAccounts);
        }


        [HttpPost("create")]
        public async Task<ActionResult<User>> AddUserAccount([FromBody] UserAddRequestDto user_dto)
        {
            try
            {
                var result = await _userService.AddUser(user_dto);
                if (result == null)
                {
                    return BadRequest("User creation failed.");
                }

                return Created("", result);
            }
            catch (Exception e)
            {
               return BadRequest(e.Message);
            }
        }
    }
}
