using ver1;
using static ver1.IDocument;
namespace Zadanie2
{
    class Program
    {
        static void Main()
        {

            IDocument doc1 = new PDFDocument("aaa.pdf");
            IDocument doc2;

            var mfDevice = new MultifunctionalDevice();
            mfDevice.PowerOn();
            mfDevice.Print(in doc1);
            IDocument doc3;
            mfDevice.Scan(out doc3);
            mfDevice.ScanAndPrint();

            mfDevice.FaxReceive(out doc3); //received fax counts as a print as well
            mfDevice.FaxSend(doc3);
            System.Console.WriteLine(mfDevice.Counter);
            System.Console.WriteLine(mfDevice.PrintCounter);
            System.Console.WriteLine(mfDevice.ScanCounter);
        }
    }
}
