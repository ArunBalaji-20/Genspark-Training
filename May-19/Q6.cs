using System;
using System.Collections.Generic;
////6) Count the Frequency of Each Element
////Given an array, count the frequency of each element and print the result.
////Input: { 1, 2, 2, 3, 4, 4, 4}
////output
////1 occurs 1 times  
////2 occurs 2 times  
////3 occurs 1 times  
////4 occurs 3 times

namespace may19
{
    internal class Class5
    {
        public static void Main(string[] args)
        {
            int[] arr = { 1, 2, 2, 3, 4, 4, 4 };

            if (arr.Length == 0 || arr == null)
            {
                Console.WriteLine("Array is empty");
            }

            Dictionary<int, int> frequency = new Dictionary<int, int>();

            foreach (int num in arr)
            {
                if (frequency.ContainsKey(num))
                {
                    frequency[num]++;
                }
                else
                {
                    frequency[num] = 1;
                }
            }

            foreach (var element in frequency)
            {
                Console.WriteLine($"{element.Key} occurs {element.Value} times");
            }
        }
    }
}
