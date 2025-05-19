using System;
using System.Threading.Channels;

//5) Take 10 numbers from user and print the number of numbers that are divisible by 7
namespace may19
{
    public class Class4
    {
        public static void Main(string[] args)
        {
            int count = 0;
            int num1, num2;

            for (int i = 1; i <= 10; i++)
            {
                Console.Write($"Enter number {i}:");
                while (!int.TryParse(Console.ReadLine(), out num1))
                    Console.WriteLine($"Invalid input. Please try again\n Enter number {i}:");


                if (num1 % 7 == 0)
                {
                    count++;
                }
            }

            Console.WriteLine($"Count of numbers divisible by 7: {count}");
        }
    }
}
