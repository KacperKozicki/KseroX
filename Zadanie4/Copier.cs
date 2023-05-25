using System;

namespace Za
{
    public interface IDevice
    {
        enum State { On, Off, Standby };

        void PowerOn();
        void PowerOff();
        void StandbyOn();
        void StandbyOff();
        State GetState();
    }

    public interface IPrinter : IDevice
    {
        void Print(in IDocument document)
        {
            if (GetState() == State.On)
            {
                Console.WriteLine($"Printing document: {document.GetFileName()}");
            }
        }
    }

    public interface IScanner : IDevice
    {
        void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            document = null;

            if (GetState() == State.On)
            {
                string fileName = $"Scan{GetScanCounter()}.{formatType.ToString().ToLower()}";
                document = new ImageDocument(fileName);

                Console.WriteLine($"Scanning document: {fileName}");
            }
        }
    }

    public interface IDocument
    {
        enum FormatType { PDF, JPG, TXT };

        string GetFileName();
    }

    public class Copier : IPrinter, IScanner
    {
        private int printCounter = 0;
        private int scanCounter = 0;
        private int documentsPrinted = 0;
        private int documentsScanned = 0;
        private State state = State.Off;

        public int Counter => printCounter + scanCounter;

        public void PowerOn()
        {
            state = State.On;
            Console.WriteLine("Copier is on ...");
        }

        public void PowerOff()
        {
            state = State.Off;
            Console.WriteLine("Copier is off ...");
        }

        public void StandbyOn()
        {
            state = State.Standby;
            Console.WriteLine("Copier is in standby mode ...");
        }

        public void StandbyOff()
        {
            state = State.On;
            Console.WriteLine("Copier is out of standby mode ...");
        }

        public State GetState()
        {
            return state;
        }

        public void Print(in IDocument document)
        {
            if (GetState() == State.On)
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

            if (GetState() == State.On)
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

        private int GetScanCounter()
        {
            return scanCounter;
        }
    }

    public class ImageDocument : IDocument
    {
        private readonly string fileName;

        public ImageDocument(string fileName)
        {
            this.fileName = fileName;
        }

        public string GetFileName()
        {
            return fileName;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Copier copier = new Copier();
            copier.PowerOn();

            IDocument document1 = new ImageDocument("Document1.jpg");
            IDocument document2 = new ImageDocument("Document2.jpg");
            IDocument document3 = new ImageDocument("Document3.jpg");

            copier.Print(document1);
            copier.Print(document2);
            copier.Print(document3);

            IDocument scannedDocument1;
            IDocument scannedDocument2;

            copier.Scan(out scannedDocument1);
            copier.Scan(out scannedDocument2);

            copier.StandbyOff();

            IDocument document4 = new ImageDocument("Document4.jpg");
            copier.Print(document4);

            copier.PowerOff();
        }
    }
}
