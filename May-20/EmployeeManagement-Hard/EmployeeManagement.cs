using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementHard
{
    internal class EmployeeManagement
    {
        Dictionary<int, Employee> _employees = new Dictionary<int, Employee>();
        InputValidatorClass validatorObj = new InputValidatorClass();
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
            
            Console.WriteLine("Enter emp id to get details:");
            int EId = validatorObj.GetInputIntOnly();
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
            name= validatorObj.GetNonEmptyStringInput();

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
            name = validatorObj.GetNonEmptyStringInput();

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

        public void DeleteEmployee()
        {
            Console.WriteLine("Enter employee Id of the employee to delete:");
            int eId= validatorObj.GetInputIntOnly();

            if(_employees.Remove(eId))
            {
                Console.WriteLine("Employee Deleted successfully.");
            }
            else
            {
                Console.WriteLine("Error , Employee not deleted.");
            }
        }

        public void ModifyEmployeeDetails()
        {
            Console.WriteLine("Enter employee Id of the employee to modify:");
            int eId = validatorObj.GetInputIntOnly();

            if (!_employees.TryGetValue(eId, out var employee))
            {
                Console.WriteLine($"No employee found with ID {eId}.");
                return;
            }

            Console.WriteLine("\nLeave field empty if you don't want to change it.");

            Console.WriteLine($"Current Name: {employee.Name}");
            Console.Write("Enter new Name: ");
            string? newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                employee.Name = newName;
            }

            Console.WriteLine($"Current Age: {employee.Age}");
            Console.Write("Enter new Age: ");
            string? newAgeInput = Console.ReadLine();
            if (int.TryParse(newAgeInput, out int newAge))
            {
                employee.Age = newAge;
            }

            Console.WriteLine($"Current Salary: {employee.Salary}");
            Console.Write("Enter new Salary: ");
            string? newSalaryInput = Console.ReadLine();
            if (double.TryParse(newSalaryInput, out double newSalary))
            {
                employee.Salary = newSalary;
            }

            Console.WriteLine("\nEmployee details updated successfully.");
        }
    }
}
