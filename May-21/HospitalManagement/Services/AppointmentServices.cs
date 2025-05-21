using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagement.Interfaces;
using HospitalManagement.Models;

namespace HospitalManagement.Services
{
    public class AppointmentServices : IAppointmentService
    {
        IRepository<int, Appointment> _appointmentRepo;

        public AppointmentServices(IRepository<int,Appointment> appointmentRepo)
        {
            _appointmentRepo = appointmentRepo;
        }
        public int AddAppointment(Appointment appointment)
        {
            try
            {
                var result = _appointmentRepo.Add(appointment);
                if (result != null)
                {
                    return result.Id;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return -1;
        }

        public List<Appointment>? SearchAppointments(SearchModel searchModel)
        {
            try
            {
                var appointments = _appointmentRepo.GetAll();
                appointments = SearchById(appointments, searchModel.Id);
                appointments = SearchByName(appointments, searchModel.PatientName);
                appointments = SeachByAge(appointments, searchModel.PatientAge);
                if (appointments != null && appointments.Count > 0)
                    return appointments.ToList(); ;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;

        }

        public bool DeleteAppointment(int id)
        {
            try
            {
                bool result=_appointmentRepo.Delete(id);
                if (result)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        private ICollection<Appointment> SeachByAge(ICollection<Appointment> appointments, Range<int>? age)
        {
            if (age == null || appointments.Count == 0 || appointments == null)
            {
                return appointments;
            }
            else
            {
                return appointments.Where(e => e.PatientAge >= age.MinVal && e.PatientAge <= age.MaxVal).ToList();
            }
        }

        private ICollection<Appointment> SearchByName(ICollection<Appointment> appointments, string? PatientName)
        {

            if (PatientName == null || appointments.Count == 0 || appointments == null)
            {
                return appointments;
            }
            else
            {
                //return appointments;
               return appointments.Where(e => e.PatientName.ToLower().Contains(PatientName.ToLower())).ToList();
            }
        }

        private ICollection<Appointment> SearchById(ICollection<Appointment> appointments, int? id)
        {
            if (id == null || appointments.Count == 0 || appointments == null)
            {
                return appointments;
            }
            else
            {
                return appointments.Where(e => e.Id == id).ToList();
            }
        }

        
    }
}
