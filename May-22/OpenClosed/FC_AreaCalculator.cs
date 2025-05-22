using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDdemo.OpenClosed
{
    //This is a faulty code it violates Open-Closed Principle, if we need to include another shape and want to
    //calculate its area then we need to modify the AreaCalculator class.So we should have a interface and implement it to avoid violation.
    public class FRectangle
    {
        public double Width { get; set; }
        public double Height { get; set; }
    }

    public class AreaCalculator
    {
        public double CalculateArea(FRectangle rectangle)
        {
            return rectangle.Width * rectangle.Height;
        }
    }

    public class FC_AreaCalculator
    {
    }
}
