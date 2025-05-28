using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstAPI.Models
{
    public class Appointment
    {
        // [Key]
        public string AppointmentNumber { get; set; } = string.Empty;
        // [ForeignKey("PatientId")]
        public int PatientId { get; set; }

        // [ForeignKey("DoctorId")]
        public int DoctorId { get; set; }
        public DateTime AppointmnetDateTime { get; set; }

        public string Status { get; set; } = string.Empty;
        public Doctor? Doctor { get; set; }
        public Patient? Patient { get; set; }
    }
}

// public class Appointment
// {
//     public int Id { get; private set; }
//     public string PatientName { get; set; } = string.Empty;
//     public int PatientAge { get; set; }
//     public DateTime AppointmentDate { get; set; }
//     public string Reason { get; set; } = string.Empty;

// }



   