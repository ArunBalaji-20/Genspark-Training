using System.Threading.Tasks;
using ChienShopMigrationProject.Dtos;
using ChienShopMigrationProject.Interfaces;
using ChienVHShopOnline.Models;
using ChienVHShopOnline.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChienShopMigrationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            var pagedOrders = await _service.GetPagedAsync(page, pageSize);
            return Ok(pagedOrders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
           
             try
            {
                var order = await _service.GetByIdAsync(id);
                return order == null ? NotFound() : Ok(order);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] OrderCreateDto order)
        {
            try
            {
                var createdOrder=await _service.AddAsync(order);
                return CreatedAtAction(nameof(Get), new { id = createdOrder.OrderID }, createdOrder);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
  
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Order order)
        {
            if (id != order.OrderID) return BadRequest();
            await _service.UpdateAsync(order);
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("payments/create")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDto paymentDto)
        {
            try
            {
                var paymentSessionId = await _service.CreatePaymentSession(paymentDto.OrderId);
                return Ok(new { PaymentSessionId = paymentSessionId, RedirectUrl = $"https://fake.payment.gateway/checkout/{paymentSessionId}" });

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpGet("payments/execute")]
        public async Task<IActionResult> ExecutePayment([FromQuery] string sessionId)
        {
            bool paymentSuccess = true;
            if (paymentSuccess)
            {
                await _service.MarkOrderAsPaidAsync(sessionId);
                return Ok(new { Message = "Payment successful" });
            }

            return BadRequest(new { Message = "Payment failed" });
        }

        [HttpGet("Export/pdf")]
        public async Task<IActionResult> ExportPDF()
        {
            try
            {
                var fileContent = await _service.ExportToPdf();
                var filename = $"OrderExport_{DateTime.Now:yyyyMMDD}.pdf";
                return File(fileContent, "application/pdf", filename);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
