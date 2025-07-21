
using Microsoft.AspNetCore.Mvc;
using videoportalAPI.Models;
using videoportalAPI.Services;

namespace videoportalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        private readonly VideoService _videoService;

        public VideosController(VideoService videoService)
        {
            _videoService = videoService;
        }
        [HttpGet("test")]
        public ActionResult<string> test()
        {
            return Ok("hello world");
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]

        public async Task<ActionResult> PostVideo(Uploaddto uploadFile)
        {
            if (uploadFile == null)
                return BadRequest("No file to upload");
            using var stream = uploadFile.file.OpenReadStream();
            await _videoService.UploadFile(stream, uploadFile.Title, uploadFile.Description);
            // return Ok("File uploaded");
            return Created();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingVideos>>> GetList()
        {
            var result = await _videoService.GetList();
            return Ok(result);
        }
    }
}