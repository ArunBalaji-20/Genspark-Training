using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDdemo.Single_Responsibility
{
    public class EmailService
    {
        public void SendEmailConfirmation(string email)
        {
            Console.WriteLine($"Order confirmation email sent to {email}");
        }
    }
}
