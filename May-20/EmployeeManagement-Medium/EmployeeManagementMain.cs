using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementMedium
{
    internal class EmployeeManagementMain
    {
        public static void Main(string[] args)
        {
            EmployeeManagement instance = new EmployeeManagement();
            instance.GetEmployeeData();
            instance.DisplayEmployeeById();
            instance.DisplaySortedEmployeesBySalary();
            instance.FindEmpolyeeByName();
            instance.FindEmployeesOlderThan();
        }
    }
}

//using employee objects
// List<Employee> EmployeePromotionsList = new List<Employee>();

//public void GetPromotionList()
//{
//    string? choice;
//    while (true)
//    {

//        Console.WriteLine("Please enter the employee names in the order of their eligibility for promotion(Please enter blank to stop,Yes to continue) ");
//        choice = Console.ReadLine();
//        if (string.IsNullOrWhiteSpace(choice))
//        {
//            break;
//        }
//        Employee employee = new Employee();

//        employee.TakeEmployeeDetailsFromUser();

//        EmployeePromotionsList.Add(employee);


//    }


//}

//public void DisplayPromotionsList()
//{
//    foreach (var emp in EmployeePromotionsList)
//    {
//        Console.WriteLine(emp.Name);

//    }
//}

//public void DisplayPositionInList()
//{
//    string? empName;
//    Console.WriteLine("Please enter the name of the employee to check promotion position:");
//    empName = Console.ReadLine();
//    int pos = 1;
//    int p = EmployeePromotionsList.FindIndex(emp => emp.Name == empName);
//    foreach (var emp in EmployeePromotionsList)
//    {
//        if (empName != emp.Name)
//        {
//            pos++;
//        }
//        else
//        {
//            break;
//        }
//    }
//    Console.WriteLine($"{empName} is the the position {pos},{p + 1} for promotion");
//}
