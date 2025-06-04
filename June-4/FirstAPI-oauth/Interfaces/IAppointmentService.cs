using FirstAPI.Models;
using FirstAPI.Models.DTOs.DoctorSpecialities;

namespace FirstAPI.Interfaces
{
    public interface IAppointmentService
    {
        public Task<Appointmnet> ScheduleAppointment(AppointmentDto appointmentDto);

        public Task<Appointmnet> CancelAppointment(string appointmentNumber);
    }
}