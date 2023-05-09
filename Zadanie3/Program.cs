using System;
using ver3;
using Zadanie3;

class Program
{
    static void Main()
    {
        var xerox = new Copier();
        xerox.PowerOn();
        IDocument doc1 = new PDFDocument("aaa.pdf");
        xerox.Print(in doc1);

        IDocument doc2;
        xerox.Scan(out doc2, IDocument.FormatType.JPG);

        xerox.ScanAndPrint();
        System.Console.WriteLine(xerox.Counter);
        System.Console.WriteLine(xerox.PrintCounter);
        System.Console.WriteLine(xerox.ScanCounter);


        System.Console.WriteLine(xerox.GetState());


        IDocument scannedDoc = new ImageDocument("ss.jpg");
        string number = "123456789";

        var multi = new MultifunctionalDevice();
        multi.PowerOn();

        multi.Scan(out scannedDoc, IDocument.FormatType.JPG);
        multi.Print(doc1);
        xerox.ScanAndPrint();
        multi.SendFax(doc1, number);
        multi.ReceiveFax(out scannedDoc, number);


        Console.WriteLine(multi.Counter);
        Console.WriteLine(multi.PrintCounter);
        Console.WriteLine(multi.ScanCounter);
        Console.WriteLine(multi.FaxCounter);
    }
}