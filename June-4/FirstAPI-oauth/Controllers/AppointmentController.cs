using FirstAPI.Interfaces;
using FirstAPI.Models;
using FirstAPI.Models.DTOs.DoctorSpecialities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{


    [ApiController]
    [Route("/api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost("Schedule")]
        public async Task<ActionResult<Appointmnet>> ScheduleAppointments([FromBody] AppointmentDto appointmentDto)
        {
            try
            {
                var newAppointment = await _appointmentService.ScheduleAppointment(appointmentDto);
                if (newAppointment != null)
                    return Created("", newAppointment);
                return BadRequest("Unable to process request at this moment");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

            // [Authorize("Doctor")]
            [Authorize(policy:"ExperiencedDoctorOnly")]
            [HttpDelete("Cancel")]
            public async Task<ActionResult<Appointmnet>> CancelAppointment( string appointmentNumber)
            {
                try
                {
                    var cancelAppointment = await _appointmentService.CancelAppointment(appointmentNumber);
                    if (cancelAppointment != null)
                        return Ok(cancelAppointment);
                    return BadRequest("Unable to process request at this moment");
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
    }
}
