using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WholeApplication.Interfaces;
using WholeApplication.Models;

namespace WholeApplication
{
    public class ManageEmployee
    {
        IEmployeeService _EmployeeService;
        public ManageEmployee(IEmployeeService employeeService) 
        {
            _EmployeeService= employeeService;


        }
        public void Start()
        {
            bool k = true;
            while (k)
            {
                int choice;
                Console.WriteLine("1.Add employees");
                Console.WriteLine("2.List employees");
                Console.WriteLine("3.Exit");
                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        var employee = new Employee();
                        employee.TakeEmployeeDetailsFromUser();
                        var res = _EmployeeService.AddEmployee(employee);
                        Console.WriteLine(res);
                        break;

                    case 2:
                        //Console.WriteLine("pending");
                        var searchModelObj = new SearchModel { Name="arun" };
                        //searchModelObj.Id = 101; alternate way create a obj and then initialize
                        var results = _EmployeeService.SearchEmployee(searchModelObj);
                        foreach (var result in results)
                        {
                            Console.WriteLine(result);
                        }
                        break;

                    case 3:
                        k = false;
                        Console.WriteLine("Exiting in 1 2 3...");
                        break;

                    default:
                        Console.WriteLine("Invalid choice,Try again");
                        break;
                }

            }
        }
    }
}
