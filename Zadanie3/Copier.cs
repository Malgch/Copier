using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ver3;

namespace Zadanie3
{
    public class Copier
    {
        private Scaner scaner = new Scaner();
        private Printer printer = new Printer();

        public int Counter { get; private set; } = 0;

        public int PrintCounter { get; private set; } = 0;
        public int ScanCounter { get; private set; } = 0;

        protected IDevice.State State = IDevice.State.off;

        public IDevice.State GetState() => State;

        #region switch ON/OFF //Copier is on if Printer or Scaner are "on".
        public void PowerOn()
        {
            if (State == IDevice.State.on && printer.GetState() == IDevice.State.on && scaner.GetState() == IDevice.State.on)
                return;

            scaner.PowerOn();
            printer.PowerOn();
            State = IDevice.State.on;
            Counter++;
        }
        public void SwitchOnPrinter() 
        {
            if (printer.GetState() == IDevice.State.on)
                return;

            printer.PowerOn();
            State = IDevice.State.on;
            Counter++;
        }
        public void SwitchOnScaner()
        {
            if (scaner.GetState() == IDevice.State.on)
                return;

            State = IDevice.State.on;
            scaner.PowerOn();            
            Counter++;
        }

        public void PowerOff()
        {
            scaner.PowerOff();
            printer.PowerOff();
            State = IDevice.State.off;
        }
        public void SwitchOffPrinter()
        {
            printer.PowerOff();

            if (scaner.GetState() == IDevice.State.off)
                State = IDevice.State.off;
        }
        public void SwitchOffScaner()
        {
            scaner.PowerOff();
            if (printer.GetState() == IDevice.State.off)
                State = IDevice.State.off;
        }
        #endregion

        public void Print(in IDocument document)
        {
            if (printer.GetState() == IDevice.State.off)
                return;

            PrintCounter++;
            printer.Print(document);
        }

        public void Scan(out IDocument document)
        {
            if (State == IDevice.State.off || scaner.GetState() == IDevice.State.off)
            {
                document = new ImageDocument("null");
                return;
            }
            scaner.Scan(out document);
            ScanCounter++;
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            if (State == IDevice.State.off || scaner.GetState() == IDevice.State.off)
            {
                document = new ImageDocument("null");
                return;
            }

            scaner.Scan(out document, formatType);
            ScanCounter++;
        }

        public void ScanAndPrint()
        {
            if (printer.GetState() == IDevice.State.off || scaner.GetState() == IDevice.State.off || State == IDevice.State.off)
                return;

            ScanCounter++;
            PrintCounter++;

            IDocument document = new ImageDocument("aaa.jpg");
            scaner.Scan(out document, formatType: IDocument.FormatType.JPG);
            printer.Print(document);
        }

    }
}
