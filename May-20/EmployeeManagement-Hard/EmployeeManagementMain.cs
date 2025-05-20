using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementHard
{
    internal class EmployeeManagementMain
    {
        public static void Main(string[] args)
        {
            EmployeeManagement instance = new EmployeeManagement();
            bool k = true;
            while (k) 
            {
                Console.WriteLine("1.Add an new employee");
                Console.WriteLine("2.Modify employee details");
                Console.WriteLine("3.Display All employees in ascending order of their salary");
                Console.WriteLine("4.Display employee using Id");
                Console.WriteLine("5.Display employee using name");
                Console.WriteLine("6.Display employees older than");
                Console.WriteLine("7.Delete employee");
                Console.WriteLine("8.Exit");

                int choice=Convert.ToInt32(Console.ReadLine());

                switch (choice) 
                {
                    case 1:
                        instance.GetEmployeeData();
                        break;

                    case 2:
                        instance.ModifyEmployeeDetails();
                        break;

                    case 3:
                        instance.DisplaySortedEmployeesBySalary();
                        break;

                    case 4:
                        instance.DisplayEmployeeById();
                        break;

                    case 5:
                        instance.FindEmpolyeeByName();
                        break;

                    case 6:
                        instance.FindEmployeesOlderThan();
                        break;

                    case 7:
                        instance.DeleteEmployee();
                        break;
                    case 8:
                        Console.WriteLine("Thank you for using the app. Exiting in 1 2 3...");
                        k = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice,Try again");
                        break ;
                }


            } 
            


        }
    }
}

