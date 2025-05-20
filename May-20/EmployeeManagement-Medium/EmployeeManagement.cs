using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementMedium
{
    internal class EmployeeManagement
    {
        Dictionary<int, Employee> _employees = new Dictionary<int, Employee>();

        public void GetEmployeeData()
        {
            string? choice;
            while (true)
            {

                Console.WriteLine("Please enter the employee details (Please enter blank to stop,Yes to continue) ");
                choice = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(choice))
                {
                    break;
                }
                Employee employee = new Employee();

                employee.TakeEmployeeDetailsFromUser();

                //1. check id is null 
                if(employee.Id ==null)
                {
                    Console.WriteLine("Employee id can't be null");
                    return;
                }
                //2. check if employee already exist
                if(_employees.ContainsKey(employee.Id))
                {
                    Console.WriteLine("Employee id already exists");
                    return;
                }
                _employees.Add(employee.Id,employee);
                Console.WriteLine("Employee added successfully");


            }
        }

        public void DisplayEmployeeById()
        {
            int EId;
            Console.WriteLine("Enter emp id to get details:");
            int.TryParse(Console.ReadLine(),out EId);
            var emp=_employees.Where(e => e.Key==EId).Select(e=> e.Value).FirstOrDefault();
            if (emp != null)
            {
                Console.WriteLine($"ID: {EId}, Name: {emp.Name}, Age: {emp.Age} , salary: {emp.Salary}"); 
            }
            else
            {
                Console.WriteLine($"No employee found with ID {EId}");
            }
        }

        public void DisplaySortedEmployeesBySalary()
        {
            var sortedEmployees = _employees.Values.ToList(); 
            sortedEmployees.Sort(); 

            foreach (var emp in sortedEmployees)
            {
                Console.WriteLine(emp);
                Console.WriteLine("-----------------------");
            }
        }

        public void FindEmpolyeeByName()
        {
            string? name;
            Console.WriteLine("Enter employee name:");
            name=Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
                return;
            }

            var matches = _employees.Values
                                    .Where(emp => emp.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                                    .ToList();

            if (matches.Any())
            {
                Console.WriteLine($"\nEmployees with the name '{name}':\n");
                foreach (var emp in matches)
                {
                    Console.WriteLine(emp);
                    Console.WriteLine("---------------------");
                }
            }
            else
            {
                Console.WriteLine($"No employees found with the name '{name}'.");
            }
        }

        public void FindEmployeesOlderThan()
        {
            string? name;
            Console.WriteLine("Enter employee name:");
            name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
                return;
            }
            var referenceEmployee = _employees.Values
                    .FirstOrDefault(emp => emp.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (referenceEmployee == null)
            {
                Console.WriteLine($"No employee found with the name '{name}'.");
                return;
            }

            int referenceAge = referenceEmployee.Age;
            var olderEmployees = _employees.Values
                    .Where(emp => emp.Age > referenceAge)
                    .ToList();
            if (olderEmployees.Any())
            {
                Console.WriteLine($"\nEmployees older than '{referenceEmployee.Name}' (Age {referenceAge}):\n");
                foreach (var emp in olderEmployees)
                {
                    Console.WriteLine(emp);
                    Console.WriteLine("------------------------");
                }
            }
            else
            {
                Console.WriteLine($"No employees are older than '{referenceEmployee.Name}'.");
            }
        }
    }
}
