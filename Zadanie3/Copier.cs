using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace ver3
{
    public class Copier : BaseDevice, IDevice
    {
       

        public IPrinter printer;
        public IScanner scanner;

        public int PrintCounter { get; private set; } = 0;
        public int ScanCounter { get; private set; } = 0;

        public new int Counter { get; private set; } = 0;

        public Copier()
        {
            scanner = new Scanner();
            printer = new Printer();
        }
    

        

        public new void PowerOn()
        {
            if (state != IDevice.State.on)
            {
                state = IDevice.State.on;
                printer.PowerOn();
                scanner.PowerOn();
                
                Console.WriteLine("Copier is on ...");
                Counter++;
            }
        }


        public new void PowerOff()
        {
            if (state == IDevice.State.on)
            {
                printer.PowerOff();
                scanner.PowerOff();
                state = IDevice.State.off;
                Console.WriteLine("...Copier is off");
            }
        }


        public void ScanAndPrint()
        {
            if (state == IDevice.State.on)
            {
                IDocument document;
                scanner.Scan(out document, IDocument.FormatType.JPG);
                if (document != null)
                {
                    printer.Print(document);
                    PrintCounter++;
                }
                ScanCounter++;
            }
        }
        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            ScanCounter++;
            scanner.Scan(out document, formatType);
               
            
        }


        public void Print(in IDocument document)
        {
            //IDocument document;
          
                PrintCounter++;
                printer.Print(in document);
               
            
        }
    }

}