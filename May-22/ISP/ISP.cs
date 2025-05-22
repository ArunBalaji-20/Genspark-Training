using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDdemo.ISP
{
    //ISP:This Principle states that Clients should not be forced to implement any methods they don’t use. Rather than one fat interface, numerous little interfaces are preferred based on groups of methods, with each interface serving one submodule.

    public interface IWorkable
    {
        void Work();
    }

    public interface IEatable
    {
        void Eat();
    }

    public class Manager : IWorkable, IEatable
    {
        public void Eat()
        {
            Console.WriteLine("Eating..");
        }

        public void Work()
        {
            Console.WriteLine("Working..");
        }
    }

    public class Robot : IWorkable
    {
        public void Work()
        {
            Console.WriteLine("Working..");
        }
    }
    public class ISP
    {
    }
}
