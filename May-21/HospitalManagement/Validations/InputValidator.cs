using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Validations
{
    public class InputValidatorClass
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

        public int GetInputAge()
        {
            while (true)
            {
                int inp;
                if (int.TryParse(Console.ReadLine(), out inp) && inp>=18)
                {
                    return inp;
                }
                else
                {
                    Console.WriteLine("Invalid input. if age less than 18 ,Not allowed.");
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
        public DateTime DateValidator()
        {
            while (true)
            {
                Console.WriteLine("Enter Appointment date and time (Please use: dd-MM-yyyy HH:mm:ss )");
                string? input = Console.ReadLine();
                DateTime AppointmentDateTime;
                if (DateTime.TryParse(input, out AppointmentDateTime) && AppointmentDateTime > DateTime.Now)
                {
                    return AppointmentDateTime;
                }
                else
                {
                    Console.WriteLine("Invalid date/time format, Appointment date should not be in past ");
                }
            }
        }

    }
}
