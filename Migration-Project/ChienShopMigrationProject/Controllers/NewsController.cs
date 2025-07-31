using ChienShopMigrationProject.Interfaces;
using ChienVHShopOnline.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChienShopMigrationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newService;

        public NewsController(INewsService newsService)
        {
            _newService = newsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            var pagedOrders = await _newService.GetPagedAsync(page, pageSize);
            return Ok(pagedOrders);
        }

        // 1. get Details
        // 2. post news
        // 3. edit news
        // 4. delete news
        // 5. export to csv

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var news = await _newService.GetByIdAsync(id);
            return news == null ? NotFound() : Ok(news);
        }

        [HttpPost("Create")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> PostNews([FromForm] NewsCreationDto news)
        {
            try
            {
                await _newService.PostNews(news);
                return CreatedAtAction(nameof(Get), new { id = news }, news);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPut("Edit/{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> update(int id, [FromForm] NewsCreationDto newsCreationDto)
        {
            try
            {
                await _newService.UpdateAsync(newsCreationDto, id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            try
            {
                await _newService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Export/CSV")]
        public async Task<IActionResult> ExportAsCsv()
        {
            try
            {
               var fileContent= await _newService.ExportToCSV();
               var fileName = $"NewsListing_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
    
                return File(fileContent, "text/csv", fileName);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
// [HttpPost]
//         [ValidateAntiForgeryToken]
//         public ActionResult Create([Bind(Include = "NewsId,UserId,Title,ShortDescription,Image,Content,CreatedDate,Status")] News news)
//         {
//             if (ModelState.IsValid)
//             {
//                 db.News.Add(news);
//                 db.SaveChanges();
//                 return RedirectToAction("Index");
//             }

//             ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", news.UserId);
//             return View(news);
//         }

// ChienVHShopDBEntities db = new ChienVHShopDBEntities();
//         // GET: News
//         public ActionResult Index()
//         {
//             return View();
//         }

//         public PartialViewResult NewsListPartial(int? page)
//         {
//             var pageNumber = page ?? 1;
//             var pageSize = 2;
//             var newsList = db.News.OrderByDescending(x => x.NewsId).ToPagedList(pageNumber, pageSize);
//             return PartialView(newsList);
//         }