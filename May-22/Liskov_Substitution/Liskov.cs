using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDdemo.Liskov_Substitution
{
    public interface IShape
    {
        int GetArea();
    }

    public class Rectangle : IShape
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int GetArea() => Width * Height;
    }

    public class Square : IShape
    {
        public int Side { get; set; }
        public int GetArea() => Side * Side;
    }

    public class Liskov
    {
        //IShape rect = new Rectangle { Width = 5, Height = 10 };
        //Console.WriteLine(rect.GetArea());  // 50

        //IShape square = new Square { Side = 10 };
        //Console.WriteLine(square.GetArea());  // 100
    }
}
