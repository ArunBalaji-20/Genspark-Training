// using System;
// using FirstAPI.Interfaces;
// using FirstAPI.Models;

// namespace FirstAPI.Services;

// public class AppointmentServices : IAppointmentServices
// {
//     IRepository<int, Appointment> _appointmentRepo;

//     public AppointmentServices(IRepository<int, Appointment> appointmentRepo)
//     {
//         _appointmentRepo = appointmentRepo;
//     }
//     public int AddAppointment(Appointment appointment)
//     {
//         try
//         {
//             var result = _appointmentRepo.Add(appointment);
//             if (result != null)
//             {
//                 return result.Id;
//             }
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e.Message);
//         }
//         return -1;
//     }

//     public bool CancelAppointment(int id)
//     {
//         bool res = _appointmentRepo.Delete(id);
//         if (res)
//         {
//             return true;
//         }
//         else
//         {
//             return false;
//         }
//     }

//     public List<Appointment>? GetAllAppointments()
//     {
//         try
//         {
//             var appointments = _appointmentRepo.GetAll();
//             if (appointments != null && appointments.Count > 0)
//                 return appointments.ToList(); ;
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e.Message);
//         }
//         return null;

//     }
// }
