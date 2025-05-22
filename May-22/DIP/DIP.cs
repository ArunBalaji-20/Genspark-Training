using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDdemo.DIP
{
    //Dependency Inversion Principle, also known as DIP. This Principle states that high-level modules/classes should not depend on low-level modules/classes. Both should depend upon abstractions. Secondly, abstractions should not depend upon details. Details should depend upon abstractions.

    using System;

    namespace SOLIDdemo.DIP
    {
        public interface ISwitchable
        {
            void TurnOn();
            void TurnOff();
        }

        public class LightBulb : ISwitchable
        {
            private bool isOn = false;

            public void TurnOn()
            {
                isOn = true;
                Console.WriteLine("LightBulb is On");
            }

            public void TurnOff()
            {
                isOn = false;
                Console.WriteLine("LightBulb is Off");
            }
        }

        public class Fan : ISwitchable
        {
            private bool isOn = false;

            public void TurnOn()
            {
                isOn = true;
                Console.WriteLine("Fan is On");
            }

            public void TurnOff()
            {
                isOn = false;
                Console.WriteLine("Fan is Off");
            }
        }

        public class Switch
        {
            private bool isOn = false;
            private ISwitchable device;

            public Switch(ISwitchable device)
            {
                this.device = device;
            }

            public void Toggle()
            {
                if (isOn)
                {
                    device.TurnOff();
                    isOn = false;
                }
                else
                {
                    device.TurnOn();
                    isOn = true;
                }
            }
        }

        public class DIP
        {
            public static void Main(string[] args)
            {
                ISwitchable bulb = new LightBulb();
                Switch bulbSwitch = new Switch(bulb);

                bulbSwitch.Toggle(); // Turns on
                bulbSwitch.Toggle(); // Turns off

                ISwitchable fan = new Fan();
                Switch fanSwitch = new Switch(fan);

                fanSwitch.Toggle(); // Turns on
                fanSwitch.Toggle(); // Turns off
            }
        }
    }


    
}
