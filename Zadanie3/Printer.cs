using System;
using System.Collections.Generic;
using System.Text;

namespace ver3
{
    public class Printer : IPrinter
    {
        private int counter = 0;
        public new int Counter => counter; 

        private IDevice.State state = IDevice.State.off;
        public int PrintCounter { get; private set; }

        public void PowerOn()
        {
            if (state == IDevice.State.off)
            {
                state = IDevice.State.on;
                Console.WriteLine("Printer is on ...");
            }
        }

        public void PowerOff()
        {
            if (state == IDevice.State.on)
            {
                state = IDevice.State.off;
                Console.WriteLine("...Printer is off");
            }
        }

        public void Print(in IDocument document)
        {
            if (state == IDevice.State.on)
            {
                PrintCounter++;
                DateTime time = DateTime.Now;
                string name = document.GetFileName();
                Console.WriteLine($"{time} Print: {name}");
            }
        }

        public IDevice.State GetState()
        {
            return state;
        }
    }
}
