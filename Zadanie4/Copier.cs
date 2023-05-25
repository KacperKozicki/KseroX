using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie4
{
    public class Copier : IPrinter, IScanner
    {
        private int printCounter = 0;
        private int scanCounter = 0;
        private int documentsPrinted = 0;
        private int documentsScanned = 0;
        private State state = State.off;

        public int Counter => printCounter + scanCounter;

        public void PowerOn()
        {
            state = State.on;
            Console.WriteLine("Copier is on ...");
        }

        public void PowerOff()
        {
            state = State.off;
            Console.WriteLine("Copier is off ...");
        }

        public void StandbyOn()
        {
            state = State.standby;
            Console.WriteLine("Copier is in standby mode ...");
        }

        public void StandbyOff()
        {
            state = State.on;
            Console.WriteLine("Copier is out of standby mode ...");
        }

        public State GetState()
        {
            return state;
        }

        public void Print(in IDocument document)
        {
            if (state == State.on)
            {
                printCounter++;
                documentsPrinted++;

                Console.WriteLine($"Printing document: {document.GetFileName()}");
                Console.WriteLine($"Total documents printed: {documentsPrinted}");

                if (documentsPrinted % 3 == 0)
                {
                    Console.WriteLine("Entering standby mode after printing 3 documents");
                    StandbyOn();
                }
            }
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            document = null;

            if (state == State.on)
            {
                scanCounter++;
                documentsScanned++;

                string fileName = $"Scan{documentsScanned}.{formatType.ToString().ToLower()}";
                document = new ImageDocument(fileName);

                Console.WriteLine($"Scanning document: {fileName}");
                Console.WriteLine($"Total documents scanned: {documentsScanned}");

                if (documentsScanned % 2 == 0)
                {
                    Console.WriteLine("Entering standby mode after scanning 2 documents");
                    StandbyOn();
                }
            }
        }
    }
}
