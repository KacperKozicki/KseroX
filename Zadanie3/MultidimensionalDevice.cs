using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Text;
using ver3;

namespace Zadanie3
{
    public class MultifunctionalDevice : Copier, IPrinter, IScanner, IFax, IDevice
    {

        public int FaxCounter { get; private set; } = 0;
        public int FaxInCounter { get; private set; } = 0;
        public int FaxFromCounter { get; private set; } = 0;
        public new int Counter { get; private set; } = 0;

        public IFax fax;
        public MultifunctionalDevice()
        {
            fax = new Fax();
        }
        public new void PowerOn()
        {
            if (state != IDevice.State.on)
            {
                state = IDevice.State.on;
                printer.PowerOn();
                scanner.PowerOn();
                fax.PowerOn();

                Console.WriteLine("Copier is on ...");
                Counter++;
            }
        }

        public void SendFax(in IDocument document, string faxNumber)
        {
            if (state == IDevice.State.on)
            {
                
                //FaxCounter++;
                //FaxInCounter++;
                fax.SendFax(document,faxNumber);
            }
        }

        public void ReceiveFax(out IDocument document, string faxNumber)
        {
            string fileName = $"FaxDocument{FaxFromCounter}";
            document = new TextDocument($"{fileName} from {faxNumber}");
            if (state == IDevice.State.on)
            {
                //FaxCounter++;
                //FaxFromCounter++;
                fax.ReceiveFax(out document, faxNumber);
            }
            
        }
    }

}
