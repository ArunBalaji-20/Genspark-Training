using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDdemo.Single_Responsibility
{
    public class Order
    {
        public List<string> Items { get; set; } = new();
        public string CustomerEmail { get; set; } = "";
    }
}
