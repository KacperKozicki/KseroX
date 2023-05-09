using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ver3
{
    public class Scanner : IScanner
    {
        private int counter = 0;
        public new int Counter => counter;

        public int ScanCounter { get; private set; }

        private IDevice.State state = IDevice.State.off;

        public void PowerOn()
        {
            if (state == IDevice.State.off)
            {
                state = IDevice.State.on;
                Console.WriteLine("Scanner is on ...");
            }
        }

        public void PowerOff()
        {
            if (state == IDevice.State.on)
            {
                state = IDevice.State.off;
                Console.WriteLine("...Scanner is off");
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
                Console.WriteLine($"{DateTime.Now.ToString()} Scan: {fileName}");
            }
        }

        public IDevice.State GetState()
        {
            return state;
        }

       
    }
}
