using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDdemo.OpenClosed
{
    public class Square : Shape
    {
        public double Length { get; set; }

        public override double CalculateArea()
        {
            double area = Length * Length;
            return area;
        }
    }
}
