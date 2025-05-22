using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDdemo.ISP
{
    public interface IFWorker
    {
        void Work();
        void Eat();
    }

    public class FManager : IFWorker
    {
        // implementation
        public void Eat()
        {
            Console.WriteLine("Eating..");
        }

        public void Work()
        {
            Console.WriteLine("Working..");
        }
    }

    public class FRobot : IFWorker
    {
        // implementation
        public void Eat()
        {
            //not required , forced to implement
            throw new NotImplementedException();
        }

        public void Work()
        {
            Console.WriteLine("working...");
        }
    }
    public class FC_ISP
    {
    }
}
