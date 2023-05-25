using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie4
{
    public interface IDevice
    {
        enum State { Off, On, Standby }

        void PowerOn();
        void PowerOff();
        void StandbyOn();
        void StandbyOff();
        State GetState();
    }

    public interface IPrinter : IDevice
    {
        void Print(IDocument document)
        {
            if (GetState() == State.On)
            {
                PrintCounter++;
                DateTime time = DateTime.Now;
                string name = document.GetFileName();
                Console.WriteLine($"{time} Print: {name}");
            }
        }

        int PrintCounter { get; }
    }

    public interface IScanner : IDevice
    {
        void Scan(out IDocument document, IDocument.FormatType formatType)
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

            if (GetState() == State.On)
            {
                ScanCounter++;
                Console.WriteLine($"{DateTime.Now.ToString()} Scan: {fileName}");
            }
        }

        int ScanCounter { get; }
    }
}