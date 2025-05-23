using DesignPatterns.Proxy;
using DesignPatterns.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DesignPatterns
{
    public class program
    {
        public static void Main(string[] args)
        {


            bool k=true;
            while (k) 
            {
                Console.WriteLine("1. Singleton example");
                Console.WriteLine("2. Proxy example");
                Console.WriteLine("3. Exit");
                int choice=Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Singleton();
                        break;

                    case 2:
                        Proxy();
                        break;

                    case 3:
                        k=false;
                        Console.WriteLine("Exiting...");
                        break;

                    default:
                        Console.WriteLine("Invalid choice..");
                        break;
                }
            }
          
        }

        public static void Singleton()
        {
            FileOperations fileOps = FileOperations.GetInstance("C:\\Notes\\may23\\sample.txt");

            fileOps.ReadContents();

            string content = "\nSample content for writing";
            fileOps.WriteContents(content);

            fileOps.ReadContents();

            fileOps.Dispose();
        }

        public static void Proxy()
        {
            //User adminUser = new User("Alice", "Admin");
            //User guestUser = new User("Bob", "Guest");
            //User normalUser = new User("Dave", "User");

            //IFile admin = new ProxyFile(adminUser);
            //Console.WriteLine(adminUser);
            //admin.Read();
            

            //IFile guest = new ProxyFile(guestUser);
            //Console.WriteLine("\n"+guestUser);
            //guest.Read();

            //IFile normal = new ProxyFile(normalUser);
            //Console.WriteLine("\n"+normalUser);
            //normal.Read();

            Console.WriteLine("Enter username:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter Role (Admin,Guest,User):");
            string role= Console.ReadLine();

            User user = new User(name, role);
            Console.WriteLine( user);
            IFile file = new ProxyFile(user);
            file.Read();
        }
        
    }
}
