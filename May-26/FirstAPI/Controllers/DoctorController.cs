using FirstAPI.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/[controller]")]
public class DoctorController : ControllerBase
{
    static List<Doctor> doctors = new List<Doctor>
    {
        new Doctor{Id=101,Name="Ramu"},
        new Doctor{Id=102,Name="Somu"},
    };
    [HttpGet]
    public ActionResult<IEnumerable<Doctor>> GetDoctors()
    {
        return Ok(doctors);
    }
    [HttpPost]
    public ActionResult<Doctor> PostDoctor([FromBody] Doctor doctor)
    {
        doctors.Add(doctor);
        return Created("", doctor);
    }

    [HttpDelete]
    public ActionResult<Doctor> DeleteDoctor(int id)
    {
        var doctor = doctors.FirstOrDefault(d => d.Id == id);
        if (doctor == null)
        {
            return NotFound();
        }
        doctors.Remove(doctor);
        return Ok(doctor);
    }

    [HttpPut]
    public ActionResult<Doctor> PutDoctor(Doctor doctor)
    {
        var ResDoctor = doctors.FirstOrDefault(d => d.Id == doctor.Id);
        if (ResDoctor == null)
        {
            return NotFound();
        }
        ResDoctor.Name = doctor.Name;

        return Ok(ResDoctor);
    }
}