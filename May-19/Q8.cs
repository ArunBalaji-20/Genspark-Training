using System;
using System.Collections.Generic;
//8) Given two integer arrays, merge them into a single array.
//Input: { 1, 3, 5}
//and {2, 4, 6}
//Output: { 1, 3, 5, 2, 4, 6}

namespace may19
{
    internal class Class7
    {
        public static void Main(string[] args)
        {
            int[] arr1 = { 1, 3, 5 };
            int[] arr2 = { 2, 4, 6 };


            int[] arr3 = new int[arr1.Length + arr2.Length];

            for (int i = 0; i < arr1.Length; i++)
            {
                arr3[i] = arr1[i];
            }
            for (int i = 0; i < arr2.Length; i++)
            { 
                arr3[arr1.Length + i] = arr2[i]; 
            }

            if(arr3.Length == 0 || arr3==null)
            {
                Console.WriteLine("Merged array is empty");
            }

            foreach (int num in arr3)
            {
                Console.Write(num + ",");
            }
            Console.WriteLine();

            Console.Write("Merged Array: ");
            for (int i = 0; i < arr3.Length; i++)
            {
                Console.Write(arr3[i]);
                if (i < arr3.Length - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine();
        }
    }
}

