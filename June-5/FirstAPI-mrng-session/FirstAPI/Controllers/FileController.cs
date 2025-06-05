using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly string _fileStoragePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

        public FileController()
        {
            if (!Directory.Exists(_fileStoragePath))
            {
                Directory.CreateDirectory(_fileStoragePath);
            }
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadFile([FromForm] FileUploadRequest request)
        {
            var file = request.File;

            if (file == null || file.Length == 0)
                return BadRequest("No file provided.");

            var path = Path.Combine(_fileStoragePath, file.FileName);

            var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);
            stream.Close();

            return Ok(new { fileName = file.FileName });
            // return Created();

        }

        [HttpGet("{fileName}")]
        public async  Task<IActionResult> GetFile(string fileName)
        {
            var path = Path.Combine(_fileStoragePath, fileName);

            if (!System.IO.File.Exists(path))
                return NotFound();
            var fileContent = await System.IO.File.ReadAllTextAsync(path);
            var contentType = "text/plain"; 

            return Content(fileContent, contentType);
        }
    }
}
