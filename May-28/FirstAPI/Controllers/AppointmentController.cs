// using FirstAPI.Interfaces;
// using FirstAPI.Models;
// using FirstAPI.Repository;
// using FirstAPI.Services;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;

// namespace FirstAPI.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class AppointmentController : ControllerBase
//     {
//         static IRepository<int, Appointment> repository = new AppointmentRepository();
//         static IAppointmentServices appointmentService = new AppointmentServices(repository);
//         [HttpGet]
//         public ActionResult<IEnumerable<Appointment>> GetAppointments()
//         {

//             return Ok(repository.GetAll());
//         }

//         [HttpPost]
//         public ActionResult<Appointment> AddAppointments([FromBody] Appointment appointment)
//         {
//             repository.Add(appointment);
//             return Created("", appointment);
//         }

//         [HttpDelete]
//         public ActionResult CancelAppointment([FromBody] int id)
//         {
//             bool res = repository.Delete(id);
//             if (res)
//             {
//                 return Ok("appointment canceled");
//             }
//             else
//             {
//                 return NotFound("id not found");
//             }
//         }
//     }
// }
