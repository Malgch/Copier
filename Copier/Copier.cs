using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static ver1.IDocument;

namespace ver1
{
    public class Copier : IPrinter, IScanner
    {
        public int PrintCounter { get; set; }

        public int ScanCounter { get; set; }

        public int Counter { get; set; }


        protected IDevice.State State = IDevice.State.off;

        public Copier()
        {
            PrintCounter = 0;
            Counter = PrintCounter + ScanCounter;
            ScanCounter = 0;
        }

        public IDevice.State GetState()
        {
            return State;
        }


        public void PowerOff()
        {
            if (State == IDevice.State.off)
                return;

            State = IDevice.State.off;
            Console.WriteLine("Device is off ...");
        }

        public void PowerOn()
        {
            if (State == IDevice.State.on)
                return;

            State = IDevice.State.on;
            Counter++;
            Console.WriteLine("Device is on ...");
        }

        public void Print(in IDocument document)
        {
            if (State == IDevice.State.off)
            {
                Console.WriteLine("Copier is off");
                return;
            }
            PrintCounter++;
            Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName().ToString()}");
            return;
        }
        public void Scan(out IDocument document)
        {
            if (State == IDevice.State.off)
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
            if (State == IDevice.State.off)
            {
                document = new ImageDocument("null");
                return;
            }        

            ScanCounter++;

            switch (formatType)
            {
                case FormatType.PDF:
                    document = new PDFDocument($"PDFScan{ScanCounter}.pdf");                    
                    break;
                case FormatType.TXT:
                    document = new TextDocument($"TextScan{ScanCounter}.txt");
                    break;
                case FormatType.JPG:
                    document = new ImageDocument($"ImageScan{ScanCounter}.jpg");
                    break;
                default:
                    throw new FormatException("Invalid format type.");
            }
            Console.WriteLine($"{DateTime.Now} Scan: {document.GetFileName().ToString()}");
        }

        public void ScanAndPrint()
        {
            if (State == IDevice.State.off)
                return;

            ScanCounter++;
            PrintCounter++;
            
            var document = new ImageDocument($"ImageScan{ScanCounter}.jpg");
            Console.WriteLine($"{DateTime.Now} Scan: {document.GetFileName().ToString()}");
            Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName().ToString()}");
        }


    }
}
