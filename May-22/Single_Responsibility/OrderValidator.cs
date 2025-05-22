using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDdemo.Single_Responsibility
{
    public class OrderValidator
    {
        public void Validate(Order order)
        {
            if (order.Items.Count == 0)
            {
                throw new InvalidOperationException("Please Select atleast one item");
            }
        }
    }
}
