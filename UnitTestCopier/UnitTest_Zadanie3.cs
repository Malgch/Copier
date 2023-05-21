using Microsoft.VisualStudio.TestTools.UnitTesting;
using ver3;
using System;
using System.IO;
using Zadanie3;

namespace ver3UnitTests
{

    public class ConsoleRedirectionToStringWriter : IDisposable
    {
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        public ConsoleRedirectionToStringWriter()
        {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        public string GetOutput()
        {
            return stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }
    }

    #region Copier tests

    [TestClass]
    public class UnitTestCopier
    {
        [TestMethod]
        public void Copier_GetState_StateOff()
        {
            var copier = new Copier();
            copier.PowerOff();

            Assert.AreEqual(IDevice.State.off, copier.GetState());
        }

        [TestMethod]
        public void Copier_GetState_StateOn()
        {
            var copier = new Copier();
            copier.PowerOn();

            Assert.AreEqual(IDevice.State.on, copier.GetState());
        }


        // weryfikacja, czy po wywołaniu metody `Print` i włączonej kopiarce w napisie pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_Print_DeviceOn()
        {
            var copier = new Copier();
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                copier.Print(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Print` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_Print_DeviceOff()
        {
            var copier = new Copier();
            copier.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                copier.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_Scan_DeviceOff()
        {
            var copier = new Copier();
            copier.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                copier.Scan(out doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_Scan_DeviceOn()
        {
            var copier = new Copier();
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                copier.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy wywołanie metody `Scan` z parametrem określającym format dokumentu
        // zawiera odpowiednie rozszerzenie (`.jpg`, `.txt`, `.pdf`)
        [TestMethod]
        public void Copier_Scan_FormatTypeDocument()
        {
            var copier = new Copier();
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                copier.Scan(out doc1, formatType: IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                copier.Scan(out doc1, formatType: IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                copier.Scan(out doc1, formatType: IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }


        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie pojawiają się słowa `Print`
        // oraz `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_ScanAndPrint_DeviceOn()
        {
            var copier = new Copier();
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                copier.ScanAndPrint();
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // ani słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_ScanAndPrint_DeviceOff()
        {
            var copier = new Copier();
            copier.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                copier.ScanAndPrint();
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void Copier_PrintCounter()
        {
            var copier = new Copier();
            copier.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            copier.Print(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            copier.Print(in doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            copier.Print(in doc3);

            copier.PowerOff();
            copier.Print(in doc3);
            copier.Scan(out doc1);
            copier.PowerOn();

            copier.ScanAndPrint();
            copier.ScanAndPrint();

            // 5 wydruków, gdy urządzenie włączone
            Assert.AreEqual(5, copier.PrintCounter);
        }

        [TestMethod]
        public void Copier_ScanCounter()
        {
            var copier = new Copier();
            copier.PowerOn();

            IDocument doc1;
            copier.Scan(out doc1);
            IDocument doc2;
            copier.Scan(out doc2);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            copier.Print(in doc3);

            copier.PowerOff();
            copier.Print(in doc3);
            copier.Scan(out doc1);
            copier.PowerOn();

            copier.ScanAndPrint();
            copier.ScanAndPrint();

            // 4 skany, gdy urządzenie włączone
            Assert.AreEqual(4, copier.ScanCounter);
        }

        [TestMethod]
        public void Copier_PowerOnCounter()
        {
            var copier = new Copier();
            copier.PowerOn();
            copier.PowerOn();
            copier.PowerOn();

            IDocument doc1;
            copier.Scan(out doc1);
            IDocument doc2;
            copier.Scan(out doc2);

            copier.PowerOff();
            copier.PowerOff();
            copier.PowerOff();
            copier.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            copier.Print(in doc3);

            copier.PowerOff();
            copier.Print(in doc3);
            copier.Scan(out doc1);
            copier.PowerOn();

            copier.ScanAndPrint();
            copier.ScanAndPrint();

            // 3 włączenia
            Assert.AreEqual(3, copier.Counter);
        }

        [TestMethod]
        public void Copier_ScanCounter_ScanerOff()
        {
            var copier = new Copier();
            copier.PowerOn();
            copier.SwitchOffScaner();

            IDocument doc1;
            copier.Scan(out doc1);
            IDocument doc2;
            copier.Scan(out doc2);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            copier.Print(in doc3);

            copier.PowerOff();
            copier.SwitchOnScaner();
            copier.Print(in doc3);
            copier.Scan(out doc1);
            copier.PowerOn();

            copier.ScanAndPrint();
            copier.ScanAndPrint();

            // 3 skany, gdy urządzenie włączone
            Assert.AreEqual(3, copier.ScanCounter);
        }
        [TestMethod]
        public void Copier_PrintCounter_PrinterOff()
        {
            var copier = new Copier();
            copier.PowerOn();
            copier.SwitchOffPrinter();

            IDocument doc1 = new ImageDocument("aaa.jpg");
            copier.Print(in doc1);
            copier.Print(in doc1);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            copier.Print(in doc3);

            copier.PowerOff();
            copier.SwitchOnPrinter();
            copier.Print(in doc3); //+1
            copier.Scan(out doc1);
            copier.PowerOn();

            copier.ScanAndPrint(); //+1
            copier.ScanAndPrint(); //+1

            // 3 skany, gdy urządzenie włączone
            Assert.AreEqual(3, copier.PrintCounter);
        }
    }
    #endregion

    #region MultifunctionalDevice Tests

    [TestClass]
    public class UnitTestMultifunctionalDevice
    {
        [TestMethod]
        public void MFDevice_GetState_StateOff()
        {
            var device = new MultifunctionalDevice();
            device.PowerOff();

            Assert.AreEqual(IDevice.State.off, device.GetState());
        }

        [TestMethod]
        public void MFDevice_GetState_StateOn()
        {
            var device = new MultifunctionalDevice();
            device.PowerOn();

            Assert.AreEqual(IDevice.State.on, device.GetState());
        }

        [TestMethod]
        public void MFDevice_WhenDeviceIsOff_ShouldPrintErrorMessage()
        {
            var device = new MultifunctionalDevice();
            device.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                device.FaxReceive(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("off"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }


        [TestMethod]
        public void MFDevice_DeviceOn_IncrementFaxCounter()
        {
            var device = new MultifunctionalDevice();
            device.PowerOn();

            IDocument doc1;
            device.FaxReceive(out doc1);
            IDocument doc2;
            device.FaxReceive(out doc2);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            device.FaxSend(in doc3);

            device.PowerOff();
            device.FaxSend(in doc3);
            device.FaxReceive(out doc1);
            device.PowerOn();

            // 4 skany, gdy urządzenie włączone
            Assert.AreEqual(3, device.FaxCounter);
        }


        [TestMethod]
        public void MFDevice_FaxReceive_DeviceOn()
        {
            var device = new MultifunctionalDevice();
            device.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                device.FaxReceive(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Fax"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Received"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MFDevice_ScanAndPrint_DeviceOn()
        {
            var mfDevice = new MultifunctionalDevice();
            mfDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                mfDevice.ScanAndPrint();
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MFDevice_PrintCounter_CopierOff()
        {
            var mfDevice = new MultifunctionalDevice();
            mfDevice.PowerOn();
            mfDevice.SwitchOffCopier();

            IDocument doc1 = new ImageDocument("aaa.jpg");
            mfDevice.Print(in doc1);
            mfDevice.Print(in doc1);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            mfDevice.Print(in doc3);

            mfDevice.PowerOff();
            mfDevice.SwitchOnCopier();
            mfDevice.Print(in doc3); //+1
            mfDevice.Scan(out doc1);
            mfDevice.PowerOn();

            mfDevice.ScanAndPrint(); //+1
            mfDevice.ScanAndPrint(); //+1

            // 3 skany, gdy urządzenie włączone
            Assert.AreEqual(3, mfDevice.PrintCounter);
        }

        [TestMethod]
        public void MFDevice_FaxCounter_FaxOff()
        {
            var mfDevice = new MultifunctionalDevice();
            mfDevice.PowerOn();
            mfDevice.SwitchOffFax();

            IDocument doc1 = new ImageDocument("aaa.jpg");
            mfDevice.FaxSend(in doc1);
            mfDevice.FaxSend(in doc1);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            mfDevice.FaxSend(in doc3);

            mfDevice.PowerOff();
            mfDevice.SwitchOnFax();
            mfDevice.FaxReceive(out doc3); //+1
            mfDevice.PowerOn();

            mfDevice.FaxSend(doc1); //+1
            mfDevice.FaxSend(doc3); //+1

            // 3 skany, gdy urządzenie włączone
            Assert.AreEqual(3, mfDevice.FaxCounter);
        }

    }
    #endregion



}