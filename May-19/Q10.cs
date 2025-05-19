using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//10) write a program that accepts a 9-element array representing a Sudoku row.

//Validates if the row:

//Has all numbers from 1 to 9.

//Has no duplicates.

//Displays if the row is valid or invalid.
namespace may19
{
    internal class Class9
    {
        int[] arr = new int[9];

        void GetInput()
        {
            for (int i = 0; i < 9; i++)
            {
                int value;
                Console.Write($"Enter number {i + 1}:");
                while (!int.TryParse(Console.ReadLine(), out value) || value < 1 || value > 9)
                    Console.WriteLine($"Invalid input. Please try again\n Enter number {i + 1}:");
                arr[i] = value;
            }

            //foreach(int i in arr)
            //{  Console.WriteLine(i+","); }
        }

        bool checkRowIsvalid()
        {
            Dictionary<int, int> frequency = new Dictionary<int, int>();

            foreach (int num in arr)
            {
                if (frequency.ContainsKey(num))
                {
                    return false;
                }
                else
                {
                    frequency[num] = 1;
                }
            }
            return true;
        }
        public static void Main(string[] args)
        {
            Class9 sudokuRows = new Class9();
            sudokuRows.GetInput();
            bool isValid = sudokuRows.checkRowIsvalid();
            if (isValid)
            {
                Console.WriteLine("The given row is valid");
            }
            else
            {

                Console.WriteLine("The given row is not valid , It contains duplicates");
            }


        }
    }
}
