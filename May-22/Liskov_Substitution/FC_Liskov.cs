using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SOLIDdemo.Liskov_Substitution
{
    //LSP: You should be able to use any subclass (FSquare) in place of its superclass (FRectangle) without breaking correctness.

//    Liskov Substitution Principle, also known as LSP.This Principle states that the object of a derived class should be able to replace an object of the base class without causing any errors in the system or modifying the behavior of the base class. That means the child class objects should be able to replace parent class objects without changing the correctness or behavior of the program.

//Key Idea: You should be able to use any subclass where you use its parent class.


    public class FRectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public int GetArea()
        {
            return Width * Height;
        }
    }

    public class FSquare : FRectangle
    {
        public override int Width
        {
            set { base.Width = base.Height = value; }
        }

        public override int Height
        {
            set { base.Width = base.Height = value; }
        }
    }

    public class FC_Liskov
    {
        //Substituting a Square for a Rectangle breaks the area calculation logic.
        //public static void Main()
        //{
        //    FRectangle rect = new FSquare();
        //    rect.Width = 5;
        //    rect.Height = 10;

        //    Console.WriteLine(rect.GetArea()); //100
        //}
    }

    //2nd faulty example
    public class Bird
    {
        public virtual void Fly() { /* implementation */ }
    }

    public class Penguin : Bird
    {
        public override void Fly()
        {
            throw new Exception("Penguins can't fly!");
        }
    }
     //Bird bird = new Ostrich(); bird.Fly(); would throw an error.
    //solution
    //public interface IFlyable
    //{
    //    void Fly();
    //}

    //public class Bird : IFlyable
    //{
    //    public void Fly()
    //    {
    //        // implementation specific to Bird
    //    }
    //}

    //public class Penguin : IFlyable
    //{
    //    public void Fly()
    //    {
    //        // implementation specific to Penguins
    //        throw new NotImplementedException("Penguins can't fly!");
    //    }
    //}
}
