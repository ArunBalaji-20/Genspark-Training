using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDdemo.OpenClosed
{
    public class ShapeMain
    {
        public static void Main(string[] args)
        {
            Rectangle rectangle = new Rectangle { height = 50, width = 10 };
            double RecArea = rectangle.CalculateArea();
            Console.WriteLine($"Area of rectangle:{RecArea}");
        }

    }
}
