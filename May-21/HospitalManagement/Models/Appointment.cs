using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagement.Validations;

namespace HospitalManagement.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public int PatientAge { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; } = string.Empty;

        public Appointment()
        {
            
        }
        public void GetAppointmentDetailsFromUser()
        {
            InputValidatorClass getInput=new InputValidatorClass();
            Console.WriteLine("Please enter Patient Name:");
            PatientName = getInput.GetNonEmptyStringInput(); //name

            Console.WriteLine("Please enter Patient Age:");
            PatientAge = getInput.GetInputAge();//age

            AppointmentDate = getInput.DateValidator(); //appointment date

            Console.WriteLine("Enter reason for appointment:");
            Reason = Console.ReadLine() ?? "";

        }

        public override string ToString()
        {
            return ($"AppointmentId: {Id} \nPatient Name:{PatientName}\nPatient Age:{PatientAge} \nAppointment date:{AppointmentDate}\nReason: {Reason}");
        }
    }
}
