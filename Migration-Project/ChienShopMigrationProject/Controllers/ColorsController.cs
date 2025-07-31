using ChienShopMigrationProject.Dtos;
using ChienShopMigrationProject.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChienShopMigrationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly IColorService _colorService;

        // 1.Create
        // 2.get
        // 3.edit
        // 4.delete
        public ColorsController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _colorService.GetByIdAsync(id);
                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateColor([FromBody] ColorAddDto colorAddDto)
        {
            try
            {
                await _colorService.AddColor(colorAddDto);
                return Created();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteColour(int id)
        {
            try
            {
                await _colorService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("Edit/{id}")]
        
        public async Task<IActionResult> UpdateColor(int id, ColorAddDto dto)
        {
            try
            {
                await _colorService.UpdateAsync(dto, id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
