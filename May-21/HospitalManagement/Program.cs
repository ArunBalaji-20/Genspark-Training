using HospitalManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagement.Models;
using HospitalManagement.Repositories;
using HospitalManagement.Services;

namespace HospitalManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IRepository<int,Appointment> repository = new AppointmentRepository();
            IAppointmentService appointmentService= new AppointmentServices(repository);
            ManageAppointment manageAppointment = new ManageAppointment(appointmentService);
            manageAppointment.Initialize();

        }
    }
}
