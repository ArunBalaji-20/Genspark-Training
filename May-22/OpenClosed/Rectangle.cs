using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDdemo.OpenClosed
{
    public class Rectangle : Shape
    {
        public double height {  get; set; }
        public double width { get; set; }
        public override double CalculateArea()
        {
            double area = height * width;
            return area;
        }
    }
}
