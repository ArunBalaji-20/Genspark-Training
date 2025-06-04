using FirstAPI.Interfaces;
using FirstAPI.Models;
using FirstAPI.Models.DTOs.DoctorSpecialities;
using FirstAPI.Repositories;

namespace FirstAPI.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IRepository<string, Appointmnet> _appointmentRepository;
        private readonly IRepository<int, Patient> _patientRepository;
        private readonly IRepository<int, Doctor> _doctorRepository;

        public AppointmentService(IRepository<string, Appointmnet> appointmentRepository,
                                IRepository<int, Doctor> doctorRepository,
                                IRepository<int, Patient> patientRepository)
        {
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
        }

        public async Task<Appointmnet> ScheduleAppointment(AppointmentDto appointmentDto)
        {
            try
            {
                var doctor = await _doctorRepository.Get(appointmentDto.DoctorId);
                var Patient = await _patientRepository.Get(appointmentDto.PatientId);

                if (doctor == null)
                {
                    throw new InvalidOperationException("Invalid Doctor id,Doctor not found");
                }

                if (Patient == null)
                {
                    throw new InvalidOperationException("Invalid patient id,patient not found");
                }
                var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                var appointment = new Appointmnet
                {
                    AppointmnetNumber = $"APPT-{timestamp}",
                    AppointmnetDateTime = appointmentDto.AppointmnetDateTime,
                    DoctorId = appointmentDto.DoctorId,
                    PatientId = appointmentDto.PatientId,
                    Status = "Scheduled"

                };

                var result = await _appointmentRepository.Add(appointment);
                return result;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }


        public async Task<Appointmnet> CancelAppointment(string appointmentNumber)
        {
            var appointment = await _appointmentRepository.Get(appointmentNumber);
            if (appointment == null)
            {
                throw new InvalidOperationException("Invalid appointment number");
            }
            var result = await _appointmentRepository.Delete(appointmentNumber);
            return result;
        }
    }
}