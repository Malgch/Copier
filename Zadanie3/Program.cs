using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ver3;

namespace Zadanie3
{
    class Program
    {
        static void Main()
        {
            var xerox = new Copier();
            xerox.PowerOn();
            IDocument doc1 = new PDFDocument("aaa.pdf");
            xerox.Print(in doc1);

            IDocument doc2;
            xerox.Scan(out doc2);

            xerox.ScanAndPrint();
            System.Console.WriteLine(xerox.Counter);
            System.Console.WriteLine(xerox.PrintCounter);
            System.Console.WriteLine(xerox.ScanCounter);

            Console.WriteLine($"{DateTime.Now}");


            var mfDevice = new MultifunctionalDevice();
            mfDevice.PowerOn();
            mfDevice.Print(in doc1);
            IDocument doc3;
            mfDevice.Scan(out doc3);
            mfDevice.ScanAndPrint();

            mfDevice.FaxReceive(out doc3);
            mfDevice.FaxSend(doc3);
            System.Console.WriteLine(mfDevice.Counter);
            System.Console.WriteLine(mfDevice.PrintCounter);
            System.Console.WriteLine(mfDevice.ScanCounter);

        }
    }
}
