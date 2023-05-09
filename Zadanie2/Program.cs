using System;
using ver2;
namespace Zadanie2
{
    internal class Program
    {
        static void Main(string[] args)
        {

           
            IDocument doc1 = new PDFDocument("aaa.pdf");
            IDocument scannedDoc;
            string number = "123456789";

            var multi = new MultifunctionalDevice();
            multi.PowerOn();
           
            multi.Scan(out scannedDoc, IDocument.FormatType.JPG);
            multi.Print(doc1);
            multi.SendFax(doc1,number); 
            multi.ReceiveFax(out scannedDoc, number);


            Console.WriteLine(multi.Counter);
            Console.WriteLine(multi.PrintCounter);
            Console.WriteLine(multi.ScanCounter);
            Console.WriteLine(multi.FaxCounter);
        }
    }
}
