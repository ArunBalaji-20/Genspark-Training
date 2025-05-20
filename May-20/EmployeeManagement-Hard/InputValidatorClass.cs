using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementHard
{
    internal class InputValidatorClass
    {
        public int GetInputIntOnly()
        {
            while (true)
            {
                int inp;
                if (int.TryParse(Console.ReadLine(), out inp))
                {
                    return inp;
                }
                else
                {
                    Console.WriteLine("Invalid input. Only numbers are allowed. Try again.");
                }
            }
        }

        public double GetInputDoubleOnly()
        {
            while (true)
            {
                if (double.TryParse(Console.ReadLine(), out double value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine("Invalid input. Only decimal numbers are allowed. Try again.");
                }
            }
        }

        public string GetNonEmptyStringInput()
        {
            while (true)
            {
                string? input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }
                else
                {
                    Console.WriteLine("Invalid input. Empty strings are not allowed. Try again.");
                }
            }

        }

    }
}
