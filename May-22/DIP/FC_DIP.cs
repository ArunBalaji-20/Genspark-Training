using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Faulty Code example
namespace SOLIDdemo.DIP
{
    public class FLightBulb
    {
        public bool  IsOn=true;
        public void TurnOn() {
            IsOn = true;
            Console.WriteLine("Bulb is On");
        }
        public void TurnOff() { 
            IsOn= false;
            Console.WriteLine("Bulb is off");
        }
    }

    public class FSwitch
    {
        private FLightBulb bulb;

        public FSwitch(FLightBulb bulb)
        {
            this.bulb = bulb;
        }

        public void Toggle()
        {
            if (bulb.IsOn)
                bulb.TurnOff();
            else
                bulb.TurnOn();
        }
    }
    public class FC_DIP
    {
        //public static void Main(string[] args) 
        //{
        //    FSwitch @switch = new FSwitch(new FLightBulb());
        //    @switch.Toggle();
        //    @switch.Toggle();
        //}
    }
}
