using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//7) create a program to rotate the array to the left by one position.
//Input: { 10, 20, 30, 40, 50}
//Output: { 20, 30, 40, 50, 10}

namespace may19
{
    internal class Class6
    {
        public static void Main(string[] args)
        {
            int[] arr = { 1, 2, 2, 3, 4, 4, 4 };

            if (arr.Length == 0 || arr == null)
            {
                Console.WriteLine("Array is empty");
                return;
            }
            int fe = arr[0];

            for (int i = 0; i < arr.Length - 1; i++)
            {
                arr[i] = arr[i + 1];
            }
            arr[arr.Length - 1] = fe;

            foreach (int i in arr)
            {
                Console.Write(i + ",");
            }
        }
    }
}
