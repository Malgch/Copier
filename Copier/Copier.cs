using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ver1.IDocument;

namespace ver1
{
    public class Copier : IPrinter, IScanner
    {
        private int PrintCounter { get; set; }

        private int ScanCounter { get; set; }

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
            Console.WriteLine("Device is on ...");
        }

        public void Print(in IDocument document)
        {
            if (State == IDevice.State.off)
            {
                Console.WriteLine("Copier is off");
                return;
            }

            Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName}.{document.GetFormatType}");
            return;
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            ScanCounter++;
            Counter++;

            switch (formatType)
            {
                case FormatType.PDF:
                    document = new PDFDocument($"PDFScan{ScanCounter}.{formatType}");
                    break;
                case FormatType.TXT:
                    document = new TextDocument($"TextScan{ScanCounter}.{formatType}");
                    break;
                case FormatType.JPG:
                    document = new ImageDocument($"ImageScan{ScanCounter}.{formatType}");
                    break;
            }

            if (State == IDevice.State.off)
            {
                document = new PDFDocument("null");
                return;
            }
            document = new ImageDocument($"ImageScan{ScanCounter}.{formatType}");


        }



    }
}
