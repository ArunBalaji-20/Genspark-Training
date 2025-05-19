using System;
//4) Take username and password from user. Check if user name is "Admin" and password is "pass" if yes then print success message. Give 3 attempts to user. In the end of eh 3rd attempt if user still is unable to provide valid creds then exit the application after print the message "Invalid attempts for 3 times. Exiting...."



namespace may19
{
    public class Class3
    {
        public static void Main(String[] args)
        {
            int n = 0;
            while (n < 3)
            {

                Console.WriteLine(n);

                Console.WriteLine("enter username:");
                string? username = Console.ReadLine();


                Console.WriteLine("enter password:");
                string? password = Console.ReadLine();

                if (username == "Admin" && password == "pass")
                {
                    Console.WriteLine("success");
                    break;
                }
                else
                {
                    Console.WriteLine("invalid");
                    n++;
                }
            }

            if (n == 3)
            {
                Console.WriteLine("Invalid attempts for 3 times. Exiting....");
            }
        }
    }
}
