using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Text;
using ver1;

namespace Zadanie2
{
    public class MultifunctionalDevice : Copier, IPrinter, IScanner, IFax
    {

        public int FaxCounter { get; private set; } = 0;
        public int FaxInCounter { get; private set; } = 0;
        public int FaxFromCounter { get; private set; } = 0;


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
