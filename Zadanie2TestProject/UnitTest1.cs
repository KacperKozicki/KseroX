using Microsoft.VisualStudio.TestTools.UnitTesting;
using ver1;
using System;
using System.IO;
using Zadanie2;


namespace Zadanie2TestProject
{
    [TestClass]
    public class MultifunctionalDeviceTests
    {
        [TestMethod]
        public void MultifunctionalDevice_PowerOnCounter()
        {
            // Arrange
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();
            multifunctionalDevice.PowerOn();
            multifunctionalDevice.PowerOn();

            // Act
            IDocument doc1;
            multifunctionalDevice.Scan(out doc1);
            IDocument doc2;
            multifunctionalDevice.Scan(out doc2);
            IDocument doc3 = new TextDocument("test.txt");
            multifunctionalDevice.SendFax(doc3, "+48123456789");
            multifunctionalDevice.SendFax(doc1, "+48123456789");
            multifunctionalDevice.PowerOff();
            multifunctionalDevice.PowerOn();
            multifunctionalDevice.PowerOff();
            multifunctionalDevice.Print(doc1);
            multifunctionalDevice.Print(doc2);
            multifunctionalDevice.Scan(out doc3);
            multifunctionalDevice.PowerOn();

            // Assert
            Assert.AreEqual(3, multifunctionalDevice.Counter);
        }

        [TestMethod]
        public void MultifunctionalDevice_Scan()
        {
            // Arrange
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            // Act
            IDocument scannedDocument;
            multifunctionalDevice.Scan(out scannedDocument, IDocument.FormatType.JPG);

            // Assert
            Assert.IsNotNull(scannedDocument);
            Assert.IsTrue(scannedDocument.GetFileName().StartsWith("ImageScan"));
            Assert.IsTrue(scannedDocument.GetFileName().EndsWith(".jpg"));
        }


        [TestMethod]
        public void MultifunctionalDevice_Print()
        {
            // Arrange
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();
            IDocument doc1 = new TextDocument("test.txt");

            // Act
            multifunctionalDevice.Print(doc1);

            // Assert
            Assert.AreEqual(1, multifunctionalDevice.PrintCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_Fax()
        {
            // Arrange
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();
            IDocument doc1 = new TextDocument("test.txt");

            // Act
            multifunctionalDevice.SendFax(doc1, "+48123456789");

            // Assert
            Assert.AreEqual(1, multifunctionalDevice.FaxCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_ScanAndPrint()
        {
            // Arrange
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();
            IDocument doc1 = new ImageDocument("test.jpg");

            // Act
            multifunctionalDevice.ScanAndPrint();

            // Assert
            Assert.AreEqual(1, multifunctionalDevice.PrintCounter);
        }

        [TestMethod]
        public void SendFax_Should_IncrementFaxCounter_When_DeviceIsOn()
        {
            // Arrange
            var device = new MultifunctionalDevice();
            var document = new TextDocument("testDocument");

            // Act
            device.PowerOn();
            device.SendFax(document, "123456789");

            // Assert
            Assert.AreEqual(1, device.FaxCounter);
        }


        [TestMethod]
        public void SendFax_StateOn_ShouldIncreaseFaxCounters()
        {
            // Arrange
            var device = new MultifunctionalDevice();
            var document = new TextDocument("testDocument");
            var faxNumber = "123456789";
            device.PowerOn();

            // Act
            device.SendFax(document, faxNumber);

            // Assert
            Assert.AreEqual(1, device.FaxCounter);
            Assert.AreEqual(1, device.FaxInCounter);
        }

        [TestMethod]
        public void SendFax_StateOff_ShouldNotIncreaseFaxCounters()
        {
            // Arrange
            var device = new MultifunctionalDevice();
            var document = new TextDocument("testDocument");
            var faxNumber = "123456789";
            device.PowerOff();

            // Act
            device.SendFax(document, faxNumber);

            // Assert
            Assert.AreEqual(0, device.FaxCounter);
            Assert.AreEqual(0, device.FaxInCounter);
        }


        [TestMethod]
        public void ReceiveFax_StateOn_ShouldIncreaseFaxCountersAndCreateDocument()
        {
            // Arrange
            var device = new MultifunctionalDevice();
            var faxNumber = "123456789";
            IDocument doc1;
            device.PowerOn();

            // Act
            device.ReceiveFax(out doc1, faxNumber);

            // Assert
            Assert.IsNotNull(doc1);
            Assert.IsInstanceOfType(doc1, typeof(TextDocument));
            Assert.IsTrue(doc1.GetFileName().StartsWith("FaxDocument"));
            Assert.AreEqual(1, device.FaxCounter);
            Assert.AreEqual(1, device.FaxFromCounter);
        }

        [TestMethod]
        public void ReceiveFax_StateOff_ShouldNotCreateDocument()
        {
            // Arrange
            var device = new MultifunctionalDevice();
            var faxNumber = "123456789";
            IDocument doc1 = null;
            device.PowerOff();

            // Act
            device.ReceiveFax(out doc1, faxNumber);

            // Assert
            Assert.IsNull(doc1);
            Assert.AreEqual(0, device.FaxCounter);
            Assert.AreEqual(0, device.FaxFromCounter);
        }


        [TestMethod]
            public void ReceiveFax_StateOff_ShouldNotIncreaseFaxCountersAndReturnNull()
            {
                // Arrange
                var device = new MultifunctionalDevice();
                var faxNumber = "123456789";
                device.PowerOff();

                // Act
                device.ReceiveFax(out var document, faxNumber);

                // Assert
                Assert.IsNull(document);
                Assert.AreEqual(0, device.FaxCounter);
                Assert.AreEqual(0, device.FaxFromCounter);
            }
        }

    
}
