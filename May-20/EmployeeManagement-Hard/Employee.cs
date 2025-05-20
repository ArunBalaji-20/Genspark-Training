using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementHard
{
    class Employee : IComparable<Employee>
    {
        int id, age;

        string name;

        double salary;

        InputValidatorClass validatorObj = new InputValidatorClass();
        public Employee()
        {

        }
        public Employee(int id, int age, string name, double salary)
        {
            this.id = id;

            this.age = age;

            this.name = name;

            this.salary = salary;

        }
        public void TakeEmployeeDetailsFromUser()
        {
            Console.WriteLine("Please enter the employee ID");

            id = validatorObj.GetInputIntOnly();

            Console.WriteLine("Please enter the employee name");

            name = validatorObj.GetNonEmptyStringInput();

            Console.WriteLine("Please enter the employee age");

            age = validatorObj.GetInputIntOnly();

            Console.WriteLine("Please enter the employee salary");

            salary = validatorObj.GetInputDoubleOnly();
        }



        public override string ToString()
        {
            return "Employee ID : " + id + "\nName : " + name + "\nAge : " + age + "\nSalary : " + salary;
        }
        public int Id { get => id; set => id = value; }
        public int Age { get => age; set => age = value; }
        public string Name { get => name; set => name = value; }
        public double Salary { get => salary; set => salary = value; }

        public int CompareTo(Employee emp2)
        {
            if (emp2 == null) return 1;
            return this.salary.CompareTo(emp2.salary); 
        }

    }

}
