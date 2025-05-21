using HospitalManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Interfaces
{
    public interface IAppointmentService
    {
        int AddAppointment(Appointment appointment);
        List<Appointment>? SearchAppointments(SearchModel searchModel);

        bool DeleteAppointment(int id);

    }
}
