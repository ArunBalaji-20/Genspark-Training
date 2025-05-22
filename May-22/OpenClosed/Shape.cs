using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDdemo.OpenClosed
{
    public abstract class Shape
    {
        public abstract double CalculateArea();
    }
}
//New shapes can be added by inherting the Shape class without modifying existing code.
