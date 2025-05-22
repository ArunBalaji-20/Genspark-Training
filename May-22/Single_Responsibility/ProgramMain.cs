using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDdemo.Single_Responsibility
{

    public class ProgramMain
    {
        public static void Main(string[] args)
        {
            var order = new Order
            {
                Items = new List<string> { "Laptop", "Mouse" },
                CustomerEmail = "abc@gmail.com"
            };

            var orderService = new OrderService();
            orderService.PlaceOrder(order);

            Console.WriteLine("Order process completed.");
        }
    }

}
