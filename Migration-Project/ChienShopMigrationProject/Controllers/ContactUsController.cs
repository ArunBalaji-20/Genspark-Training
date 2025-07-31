using ChienShopMigrationProject.Interfaces;
using ChienVHShopOnline.Models;
using ChienVHShopOnline.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChienShopMigrationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IContactUsService _contactUsService;



        public ContactUsController(IContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }

        // 1.Add
        // 2.edit
        // 3.delete
        // 4.View

        [HttpPost("Create")]
        public async Task<ActionResult> Submit([FromBody] SubmitContactUsDto contact)
        {
            try
            {
                await _contactUsService.Submit(contact);
                return Created();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Details/{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var result = await _contactUsService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("Edit/{id}")]
        public async Task<ActionResult> EditData([FromBody] SubmitContactUsDto contact, int id)
        {
            try
            {
                await _contactUsService.UpdateAsync(contact, id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _contactUsService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        


    }
}
