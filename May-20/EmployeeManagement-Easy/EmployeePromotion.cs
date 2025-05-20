using EmployeeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EmployeeManagement
{
    public class EmployeePromotion
    {
        List<string> EmployeePromotionsList = new List<string>();

        public void GetPromotionList()
        {
            string? choice;
            string? name;
            while (true)
            {

                Console.WriteLine("Please enter the employee names in the order of their eligibility for promotion(Please enter blank to stop,Yes to continue) ");
                choice = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(choice))
                {
                    break;
                }
                Console.WriteLine("enter employee name:");
                name= Console.ReadLine();
                EmployeePromotionsList.Add(name);

            }
            Console.WriteLine($"The current size of the collection is {EmployeePromotionsList.Capacity}");

            //EmployeePromotionsList.RemoveRange(EmployeePromotionsList.Count - 3, 3);
            Console.WriteLine($"The current size of the collection(after removing 3) is {EmployeePromotionsList.Capacity}");
            EmployeePromotionsList.TrimExcess();

            Console.WriteLine($"The size after removing the extra space is {EmployeePromotionsList.Capacity}");
        }

        public void DisplayPromotionList() 
        {
            foreach (string name in EmployeePromotionsList)
            {
                Console.WriteLine(name);
            }
        }

        public void DisplayPositionInList()
        {
            string? empName;
            Console.WriteLine("Please enter the name of the employee to check promotion position:");
            empName = Console.ReadLine();

            int pos=EmployeePromotionsList.IndexOf(empName)+1;
            Console.WriteLine($"{empName} is the the position {pos} for promotion");
        }

        public void PromotedList()
        {
            EmployeePromotionsList.Sort();
            Console.WriteLine("Promoted Employees List (in asc order):");
            DisplayPromotionList();
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