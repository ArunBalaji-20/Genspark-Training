using ChienShopMigrationProject.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChienShopMigrationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            var pagedOrders = await _productService.GetPagedAsync(page, pageSize);
            return Ok(pagedOrders);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            return product == null ? NotFound() : Ok(product);
        }
    }
}


//  public PartialViewResult ProductListPartial(int? page, int? category)
//         {
//             var pageNumber = page ?? 1;
//             var pageSize = 10;
//             if (category != null)
//             {
//                 ViewBag.category = category;
//                 var productList = db.Products
        //                         .OrderByDescending(x => x.ProductId)
        //                         .Where(x=> x.CategoryId == category)
        //                         .ToPagedList(pageNumber, pageSize);
        //         return PartialView(productList);
        //     }
        //     else
        //     {
        //         var productList = db.Products.OrderByDescending(x => x.ProductId).ToPagedList(pageNumber, pageSize);
        //         return PartialView(productList);
        //     }
        // }

        // // GET: Products/Details/5
        // public ActionResult Details(int? id)
        // {
        //     if (id == null)
        //     {
        //         return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //     }
        //     Product product = db.Products.Find(id);
        //     if (product == null)
        //     {
        //         return HttpNotFound();
        //     }
        //     return View(product);