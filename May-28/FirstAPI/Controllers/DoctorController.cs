using FirstAPI.Models;
using FirstAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using FirstAPI.Interfaces;

[ApiController]
[Route("/api/[controller]")]
public class DoctorController : ControllerBase
{
      private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost]
        public async Task<IActionResult> AddDoctor([FromBody] DoctorAddRequestDto doctorDto)
        {
            if (doctorDto == null)
                return BadRequest("Doctor data is null.");

            try
            {
                var createdDoctor = await _doctorService.AddDoctor(doctorDto);
                return Created(string.Empty, createdDoctor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("byname/{name}")]
        public async Task<IActionResult> GetDoctorByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name is required.");

            var doctor = await _doctorService.GetDoctByName(name);

            if (doctor == null)
                return NotFound($"Doctor with name '{name}' not found.");

            return Ok(doctor);
        }

        [HttpGet("byspeciality/{speciality}")]
        public async Task<IActionResult> GetDoctorsBySpeciality(string speciality)
        {
            if (string.IsNullOrWhiteSpace(speciality))
                return BadRequest("Speciality is required.");

            try
            {
                var doctors = await _doctorService.GetDoctorsBySpeciality(speciality);

                if (doctors == null || doctors.Count == 0)
                    return NotFound($"No doctors found with speciality '{speciality}'.");

                return Ok(doctors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    // static List<Doctor> doctors = new List<Doctor>
    // {
    //     new Doctor{Id=101,Name="Ramu"},
    //     new Doctor{Id=102,Name="Somu"},
    // };
    // [HttpGet]
    // public ActionResult<IEnumerable<Doctor>> GetDoctors()
    // {
    //     return Ok(doctors);
    // }
    // [HttpPost]
    // public ActionResult<Doctor> PostDoctor([FromBody] Doctor doctor)
    // {
    //     doctors.Add(doctor);
    //     return Created("", doctor);
    // }

    // [HttpDelete]
    // public ActionResult<Doctor> DeleteDoctor(int id)
    // {
    //     var doctor = doctors.FirstOrDefault(d => d.Id == id);
    //     if (doctor == null)
    //     {
    //         return NotFound();
    //     }
    //     doctors.Remove(doctor);
    //     return Ok(doctor);
    // }

    // [HttpPut]
    // public ActionResult<Doctor> PutDoctor(Doctor doctor)
    // {
    //     var ResDoctor = doctors.FirstOrDefault(d => d.Id == doctor.Id);
    //     if (ResDoctor == null)
    //     {
    //         return NotFound();
    //     }
    //     ResDoctor.Name = doctor.Name;

    //     return Ok(ResDoctor);
    // }
}