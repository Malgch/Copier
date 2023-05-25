using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ver3;

namespace Zadanie3
{
    #region Printer class
    public class Printer : BaseDevice, IPrinter
    {
        public int PrintCounter { get; protected set; } = 0;

        public Printer()
        {
            PrintCounter = 0;
        }

        public void Print(in IDocument document)
        {
            if (state == IDevice.State.off)
            {
                Console.WriteLine("Device is off");
                return;
            }
            PrintCounter++;
            Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName().ToString()}");
            return;
        }
    }
    #endregion

    #region Scaner class
    public class Scaner : BaseDevice, IScanner
    {
        public int ScanCounter { get; protected set; } = 0;

        public void Scan(out IDocument document)
        {
            if (state == IDevice.State.off)
            {
                document = new ImageDocument("");
                return;
            }
            else
            {
                ScanCounter++;
                document = new ImageDocument($"ImageScan{ScanCounter}.jpg");
                Console.WriteLine($"{DateTime.Now} Scan: {document.GetFileName().ToString()}");
            }
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            if (state == IDevice.State.off)
            {
                document = new ImageDocument("null");
                return;
            }

            ScanCounter++;

            switch (formatType)
            {
                case IDocument.FormatType.PDF:
                    document = new PDFDocument($"PDFScan{ScanCounter}.pdf");
                    break;
                case IDocument.FormatType.TXT:
                    document = new TextDocument($"TextScan{ScanCounter}.txt");
                    break;
                case IDocument.FormatType.JPG:
                    document = new ImageDocument($"ImageScan{ScanCounter}.jpg");
                    break;
                default:
                    throw new FormatException("Invalid format type.");
            }
            Console.WriteLine($"{DateTime.Now} Scan: {document.GetFileName().ToString()}");
        }
    }
    #endregion

    #region Fax class

    public class Fax : BaseDevice, IFax
    {
        public int FaxCounter { get; protected set; } = 0;
        public void FaxReceive(out IDocument document, IDocument.FormatType formatType)
        {
            if (state == IDevice.State.off)
            {
                document = new ImageDocument("");
                return;
            }
            else
            {
                FaxCounter++;
                document = new ImageDocument($"ImageFax{FaxCounter}.jpg");
                Console.WriteLine($"{DateTime.Now} Received Fax: {document.GetFileName().ToString()}");
            }
        }

        public void FaxSend(in IDocument document)
        {
            if (state == IDevice.State.off)
            {
                Console.WriteLine("Device is off");
                return;
            }

            FaxCounter++;
            Console.WriteLine($"{DateTime.Now} Sent Fax: {document.GetFileName().ToString()}");
            return;
        }
    }


    #endregion
}
