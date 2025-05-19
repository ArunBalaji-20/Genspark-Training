using System;


//2) Take 2 numbers from user and print the largest

public class Demo
{
    public static void Main(String[] args)
    {
        int n1;
        int n2;
        Console.Write("enter number 1:");
        while (!int.TryParse(Console.ReadLine(), out n1))
            Console.WriteLine($"Invalid input. Please try again\nenter number 1:");

        Console.Write("enter number 2:");
        while (!int.TryParse(Console.ReadLine(), out n2))
            Console.WriteLine($"Invalid input. Please try again\nenter number 2:");

        if (n1 > n2)
        {
            Console.WriteLine("Largest number is : " + n1);
        }
        else if (n1 < n2)
        {
            Console.WriteLine("Largest number is : " + n2);
        }
        else
        {
            Console.WriteLine("Both are equal");
        }
    }
}