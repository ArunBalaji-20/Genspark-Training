using HospitalManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagement.Validations;
using HospitalManagement.Models;

namespace HospitalManagement
{
    public class ManageAppointment
    {
        IAppointmentService _appointmentService;
        public ManageAppointment(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        InputValidatorClass getInput = new InputValidatorClass();

        public void Initialize()
        {
            bool k = true;
            while (k)
            {
                Console.WriteLine("1. Add appointments");
                Console.WriteLine("2. Search appointments(by id,name)");
                Console.WriteLine("3. Delete appointments");
                Console.WriteLine("4. Exit");

                int choice = getInput.GetInputIntOnly();

                switch (choice)
                {
                    case 1:
                        AddAppointmets();
                        break;

                    case 2:
                        SearchAppointments();

                        break;

                    case 3:
                        DeleteAppointments();
                        break;

                    case 4:
                        Console.WriteLine("Exiting in 1 2 3..");
                        k = false;
                        break;

                    default:
                        Console.WriteLine("invalid choice , try again");
                        break;
                }

            }
        }

        public void AddAppointmets()
        {
            var appointment = new Appointment();
            appointment.GetAppointmentDetailsFromUser();
            var result = _appointmentService.AddAppointment(appointment);
            Console.WriteLine($"Appointment booked successfully, Your Appointment ID:{result}");
        }
        public void DeleteAppointments()
        {
            Console.WriteLine("Enter appointment id to delete (ex:101):");
            int appId = getInput.GetInputIntOnly();
            bool del_result = _appointmentService.DeleteAppointment(appId);
            if (del_result)
            {
                Console.WriteLine($"Appointment Id {appId} deleted successfully");
            }
            else
            {
                Console.WriteLine($"Error deleting appointment.");
            }
        }
        public void SearchAppointments()
        {
            bool i = true;
            while (i)
            {
                int choice;
                Console.WriteLine("1. Search by Patient Name");
                Console.WriteLine("2. Search by Patient Id");
                Console.WriteLine("3. Search by Patient age");
                Console.WriteLine("4. Back to main menu");
                choice =getInput.GetInputIntOnly();

                SearchModel searchModel = new SearchModel ();

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter patient Name to search:");
                        searchModel.PatientName=getInput.GetNonEmptyStringInput();
                        var results = _appointmentService.SearchAppointments(searchModel);
                        if(results==null)
                        {
                            Console.WriteLine("No records found");
                            break;
                        }
                        foreach (var res in results)
                        {
                            Console.WriteLine(res);
                        }
                        break;
                    
                    case 2:
                        Console.WriteLine("Enter Appoitment ID to search:");
                        searchModel.Id = getInput.GetInputIntOnly();
                        var results_id = _appointmentService.SearchAppointments(searchModel);
                        if (results_id == null)
                        {
                            Console.WriteLine("No records found");
                            break ;
                        }
                        foreach (var res in results_id)
                        {
                            Console.WriteLine(res);
                        }
                        break;

                    case 3:
                        //Console.WriteLine("pending");
                        searchModel.PatientAge = new Range<int>();
                        Console.WriteLine("Enter patient Minimum age :");
                        int minAge = getInput.GetInputAge();
                        searchModel.PatientAge.MinVal = minAge;

                        Console.WriteLine("Enter patient Maximum age :");
                        int maxAge = getInput.GetInputAge();
                        searchModel.PatientAge.MaxVal = maxAge;

                        var results_age = _appointmentService.SearchAppointments(searchModel);
                        if (results_age == null)
                        {
                            Console.WriteLine("No records found");
                            break;
                        }
                        foreach (var res in results_age)
                        {
                            Console.WriteLine(res);
                        }
                        break;


                    case 4:
                        i= false;
                        Console.WriteLine("Back to main menu");
                        break;

                    default:
                        Console.WriteLine("invalid choice,Try again");
                        break;

                }
            }
        }
    }
}
