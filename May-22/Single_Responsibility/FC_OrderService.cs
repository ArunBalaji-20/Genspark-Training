using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDdemo.Single_Responsibility
{
    //This example violates SRP , as it performs too much of functionalities in one class.
    public class FC_OrderService
    {
        public void PlaceOrder(Order order)
        {
            // checking items exists
            if(order.Items.Count ==0)
            {
                throw new InvalidOperationException("Please Select atleast one item");
            }

            //if present place order
            Console.WriteLine("Order placed ");

            //send email confirmation 
            Console.WriteLine($"Order confirmation email sent to {order.CustomerEmail}");


        }
    }
   
}
