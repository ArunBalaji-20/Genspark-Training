using WholeApplication.Interfaces;
using WholeApplication.Models;
using WholeApplication.Repositories;
using WholeApplication.Services;

namespace WholeApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Employee employee = new Employee(101, 22, "Ramu", 123456); ;
            //Console.WriteLine(employee);
            
            //IRepositor<int,Employee> repositor = new EmployeeRepository();
          //  var addedEmployee = repositor.Add(employee);
            
            //Console.WriteLine(addedEmployee);

            //IEmployeeService EmpSObj = new EmployeeService(repositor);
            //var res=EmpSObj.AddEmployee(employee);
            //Console.WriteLine(res);

            IRepositor<int, Employee> employeeRepository = new EmployeeRepository();
            IEmployeeService employeeService = new EmployeeService(employeeRepository);
            ManageEmployee manageEmployee = new ManageEmployee(employeeService);
            manageEmployee.Start();

            



        }
    }
}