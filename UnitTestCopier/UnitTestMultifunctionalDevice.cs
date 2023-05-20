using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ver1;
using Zadanie2;

namespace UnitTestMultifunctionalDevice
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
                    device.FaxSend(in doc1);
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
                device.FaxReceive(out doc1, formatType: IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Fax"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Received"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }



    }



}
