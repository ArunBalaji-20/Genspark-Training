using FirstAPI.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/[controller]")]
public class PatientController : ControllerBase
{
    static List<Patient> patients = new List<Patient>
    {
        new Patient{Id=101,Name="Arun",Age=20,Contact=122142},
        new Patient{Id=102,Name="Balaji",Age=25,Contact=12322}
    };

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult<IEnumerable<Patient>> GetAllPatients()
    {
        if (patients != null)
        {
            return Ok(patients);
        }
        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Patient> PostPatient([FromBody] Patient patient)
    {
        if (patient != null)
        {
            var IdCheck = patients.FirstOrDefault(p => p.Id == patient.Id);
            if (IdCheck != null)
            {
                return BadRequest("Id already exists.");
            }
            if (patient.Age < 0)
            {
                return BadRequest("Age cannot be less than zero");
            }

            patients.Add(patient);
            return Created("", patient);
        }
        return NoContent();

    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public ActionResult<Patient> DeletePatient(int id)
    {
        var patient = patients.FirstOrDefault(p => p.Id == id);
        if (patient == null)
        {
            return NotFound();
        }
        patients.Remove(patient);
        return Ok(patient);

    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Patient> PutPatients(Patient patient)
    {
        var ModifyPatient = patients.FirstOrDefault(p => p.Id == patient.Id);
        if (ModifyPatient == null)
        {
            return NotFound();
        }
        ModifyPatient.Name = patient.Name;
        ModifyPatient.Age = patient.Age;
        ModifyPatient.Contact = patient.Contact;


        return Ok(ModifyPatient);
    }
}