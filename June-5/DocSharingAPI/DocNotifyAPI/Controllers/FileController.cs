using DocNotifyAPI.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;


namespace DocNotifyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly string _fileStoragePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
        private readonly IHubContext<NotificationHub> _hubContext;

        public FileController(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
            if (!Directory.Exists(_fileStoragePath))
            {
                Directory.CreateDirectory(_fileStoragePath);
            }
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        [Authorize(Roles ="HR-Admin")]
        public async Task<IActionResult> UploadFile([FromForm] FileUploadRequest request)
        {
            var file = request.File;

            if (file == null || file.Length == 0)
                return BadRequest("No file provided.");

            var path = Path.Combine(_fileStoragePath, file.FileName);

            var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);
            stream.Close();

            System.Console.WriteLine("Before hub call");
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", $"New document uploaded: {file.FileName}");
            System.Console.WriteLine("after hub call");

            return Ok(new { fileName = file.FileName });
            // return Created();

        }

        [HttpGet("{fileName}")]
        public async Task<IActionResult> GetFile(string fileName)
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
