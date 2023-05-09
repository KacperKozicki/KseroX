using Microsoft.VisualStudio.TestTools.UnitTesting;
using ver3;

namespace Zadanie3TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestPowerOn()
        {
            // arrange
            Copier copier = new Copier();
            int expectedCounter = 1;

            // act
            copier.PowerOn(); copier.PowerOn();


            // assert
            Assert.AreEqual(expectedCounter, copier.Counter);
            Assert.AreEqual(IDevice.State.on, copier.GetState());
        }

        [TestMethod]
        public void TestPowerOff()
        {
            // arrange
            Copier copier = new Copier();
            copier.PowerOn();

            // act
            copier.PowerOff();

            // assert
            Assert.AreEqual(IDevice.State.off, copier.GetState());
        }

        [TestMethod]
        public void TestScanAndPrint()
        {
            // arrange
            Copier copier = new Copier();
            copier.PowerOn();
            // act
            IDocument document = new ImageDocument("test.jpg");
            copier.ScanAndPrint();

            // assert
            Assert.AreEqual(1, copier.PrintCounter);
            Assert.AreEqual(1, copier.ScanCounter);
        }
       

        [TestMethod]
        public void TestScan()
        {
            // arrange
            Copier copier = new Copier();
            IDocument document;
            IDocument.FormatType formatType = IDocument.FormatType.JPG;

            // act
            copier.Scan(out document, formatType);

            // assert
            Assert.IsInstanceOfType(document, typeof(ImageDocument));
            Assert.AreEqual(formatType, document.GetFormatType());
        }

        [TestMethod]
        public void TestPrint()
        {
            // arrange
            Copier copier = new Copier();
            IDocument document = new ImageDocument("test.jpg");
            int expectedPrintCounter = 1;

            // act
            copier.Print(document);

            // assert
            Assert.AreEqual(expectedPrintCounter, copier.PrintCounter);
        }
        ///

        [TestMethod]
        public void ScanAndPrint_Should_IncreasePrintCounter_When_DeviceIsOn()
        {
            // Arrange
            var device = new Copier();
            var document = new TextDocument("testDocument");

            // Act
            device.PowerOn();
            device.ScanAndPrint();

            // Assert
            Assert.AreEqual(1, device.PrintCounter);
        }

        [TestMethod]
        public void ScanAndPrint_StateOn_ShouldIncreasePrintCounters()
        {
            // Arrange
            var device = new Copier();
            var document = new TextDocument("testDocument");
            device.PowerOn();

            // Act
            device.ScanAndPrint();

            // Assert
            Assert.AreEqual(1, device.PrintCounter);
            Assert.AreEqual(1, device.ScanCounter);
        }

        [TestMethod]
        public void ScanAndPrint_StateOff_ShouldNotIncreasePrintCounters()
        {
            // Arrange
            var device = new Copier();
            var document = new TextDocument("testDocument");
            device.PowerOff();

            // Act
            device.ScanAndPrint();

            // Assert
            Assert.AreEqual(0, device.PrintCounter);
            Assert.AreEqual(0, device.ScanCounter);
        }

        [TestMethod]
        public void PowerOn_ShouldPowerOnScannerAndPrinter()
        {
            // Arrange
            var device = new Copier();

            // Act
            device.PowerOn();

            // Assert
            Assert.AreEqual(IDevice.State.on, device.scanner.GetState());
            Assert.AreEqual(IDevice.State.on, device.printer.GetState());
        }

        [TestMethod]
        public void PowerOff_ShouldPowerOffScannerAndPrinter()
        {
            // Arrange
            var device = new Copier();

            // Act
            device.PowerOff();

            // Assert
            Assert.AreEqual(IDevice.State.off, device.scanner.GetState());
            Assert.AreEqual(IDevice.State.off, device.printer.GetState());
        }

        [TestMethod]
        public void Scan_Should_IncreaseScanCounter_When_DeviceIsOn()
        {
            // Arrange
            var device = new Copier();
            IDocument document;
            IDocument.FormatType formatType = IDocument.FormatType.JPG;
            // Act
            device.PowerOn();
            device.Scan(out document, formatType);

            // Assert
            Assert.AreEqual(1, device.ScanCounter);
        }

        [TestMethod]
        public void Print_Should_IncreasePrintCounter_When_DeviceIsOn()
        {
            // Arrange
            var device = new Copier();
            IDocument doc1 = new TextDocument("test.txt");

            // Act
            device.PowerOn();
            device.Print(doc1);

            // Assert
            Assert.AreEqual(1, device.PrintCounter);
        }
    }
}
