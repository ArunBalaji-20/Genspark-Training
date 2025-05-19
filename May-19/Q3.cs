using System;
//3) Take 2 numbers from user, check the operation user wants to perform (+,-,*,/). Do the operation and print the result




namespace may19
{
    public class Class2
    {
        public static void Main(String[] args)
        {
            int n1;
            int n2;
            System.Console.WriteLine("enter number 1:");

            n1 = Convert.ToInt32(Console.ReadLine());
            System.Console.WriteLine("enter number 2:");
            n2 = Convert.ToInt32(Console.ReadLine());

            string op;
            Console.WriteLine("enter operation to perform(ADD,SUB,MUL,DIV):");
            op = Console.ReadLine();

            switch (op)
            {
                case "ADD":
                    Console.WriteLine(n1 + n2);
                    break;
                case "SUB":
                    Console.WriteLine(n1 - n2);
                    break;
                case "MUL":
                    Console.WriteLine(n1 * n2);
                    break;
                case "DIV":
                    Console.WriteLine(n1 / n2);
                    break;
                default:
                    Console.WriteLine("enter correct operation");
                    break;
            }

        }
    }
}
