using System;
using System.Collections.Generic;
using System.Text;
using ver3;


namespace Zadanie3
{
    public class Fax : IFax, IDevice
    {
        public int FaxCounter { get; private set; } = 0;
        public int FaxInCounter { get; private set; } = 0;
        public int FaxFromCounter { get; private set; } = 0;
        private int counter = 0;
        public new int Counter => counter;

        private IDevice.State state = IDevice.State.off;
        public int PrintCounter { get; private set; }

        public void PowerOn()
        {
            if (state == IDevice.State.off)
            {
                state = IDevice.State.on;
                Console.WriteLine("Fax is on ...");
            }
        }

        public void PowerOff()
        {
            if (state == IDevice.State.on)
            {
                state = IDevice.State.off;
                Console.WriteLine("...Fax is off");
            }
        }
        public IDevice.State GetState()
        {
            return state;
        }

        public void SendFax(in IDocument document, string faxNumber)
        {
            if (state == IDevice.State.on)
            {
                Console.WriteLine($"{DateTime.Now.ToString()} Send Fax: {document.GetFileName()} to {faxNumber}");
                FaxCounter++;
                FaxInCounter++;
            }
        }

        public void ReceiveFax(out IDocument document, string faxNumber)
        {
            if (state == IDevice.State.on)
            {
                FaxCounter++;
                FaxFromCounter++;

                string fileName = $"FaxDocument{FaxFromCounter}";
                document = new TextDocument($"{fileName} from {faxNumber}");
                Console.WriteLine($"{DateTime.Now.ToString()} Receive Fax: {document.GetFileName()}");
            }
            else
            {
                document = null;
            }
        }
    }
}
