using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDdemo.Single_Responsibility
{
    public class OrderRepository
    {
        public void SaveOrder(Order order)
        {
            Console.WriteLine("Order saved to database.");
        }
    }
}
