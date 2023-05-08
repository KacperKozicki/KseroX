using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ver1
{
    public class Copier : BaseDevice, IPrinter, IScanner, IDevice
    {
        public int PrintCounter { get; private set; } = 0;
        public int ScanCounter { get; private set; } = 0;
        public Copier()
        {
            
        }


        private int counter = 0;
        public new int Counter => counter;

        public new void PowerOn()
        {
            if (state != IDevice.State.on)
            {
                counter++;
            }
            state = IDevice.State.on;
            Console.WriteLine("Device is on ...");
            
            
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

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {


            string fileType;
            switch (formatType)
            {
                case IDocument.FormatType.PDF:
                    fileType = "PDF";
                    break;
                case IDocument.FormatType.JPG:
                    fileType = "Image";
                    break;
                case IDocument.FormatType.TXT:
                    fileType = "Text";
                    break;
                default:
                    fileType = "Unknown";
                    break;
            }

            string fileName = $"{fileType}Scan{ScanCounter}.{formatType.ToString().ToLower()}";



            document = new ImageDocument(fileName);

            if (state == IDevice.State.on)
            {
                ScanCounter++;
                Console.WriteLine($"{DateTime.Now.ToString()} Scan: {fileName}");
            }



        }


        public void ScanAndPrint()
        {
            IDocument document;
            Scan(out document);
            if (document != null)
            {
                Print(document);
            }
        }
    }
}
