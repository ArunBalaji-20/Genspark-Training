namespace FirstAPI.Models.DTOs.DoctorSpecialities
{
    public class AppointmentDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmnetDateTime { get; set; }
    }
}
