using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ver3;

namespace Zadanie3
{
    public class MultifunctionalDevice
    {
        private Copier copier = new Copier();
        private Fax fax = new Fax();


        public int Counter { get; private set; } = 0;
        public int FaxCounter { get; protected set; } = 0;
        public int PrintCounter { get; protected set; } = 0;
        public int ScanCounter { get; protected set; } = 0;

        public MultifunctionalDevice()
        {
            FaxCounter = fax.FaxCounter;
            PrintCounter = copier.PrintCounter;
            ScanCounter = copier.ScanCounter;
        }

        protected IDevice.State State = IDevice.State.off;
        public IDevice.State GetState() => State;

        #region switch ON/OFF //MF Device is on if Copier or Fax are "on".
        public void PowerOn()
        {
            if (State == IDevice.State.on && copier.GetState() == IDevice.State.on && fax.GetState() == IDevice.State.on)
                return;

            copier.PowerOn();
            fax.PowerOn();
            State = IDevice.State.on;
            Counter++;
        }
        public void SwitchOnCopier()
        {
            if (copier.GetState() == IDevice.State.on)
                return;

            copier.PowerOn();
            State = IDevice.State.on;
            Counter++;
        }
        public void SwitchOnFax()
        {
            if (fax.GetState() == IDevice.State.on)
                return;

            State = IDevice.State.on;
            fax.PowerOn();
            Counter++;
        }

        public void PowerOff()
        {
            fax.PowerOff();
            copier.PowerOff();
            State = IDevice.State.off;
        }
        public void SwitchOffCopier()
        {
            copier.PowerOff();

            if (fax.GetState() == IDevice.State.off)
                State = IDevice.State.off;
        }
        public void SwitchOffFax()
        {
            fax.PowerOff();
            if (copier.GetState() == IDevice.State.off)
                State = IDevice.State.off;
        }
        #endregion

        #region FAX options
        public void FaxReceive(out IDocument document)
        {
            if (State == IDevice.State.off || fax.GetState() == IDevice.State.off)
            {
                document = new ImageDocument("");
                Console.WriteLine("Device is off");
                return;
            }

            fax.FaxReceive(out document, formatType : IDocument.FormatType.JPG);
            FaxCounter++;
        }
        public void FaxSend(in IDocument document)
        {
            if (State == IDevice.State.off || fax.GetState() == IDevice.State.off)
                return;            
            fax.FaxSend(document);
            FaxCounter++;
        }
        #endregion

        #region Print options

        public void Print(in IDocument document)
        {
            if (State == IDevice.State.off || copier.GetState() == IDevice.State.off)
            {
                Console.WriteLine("Device is off");
                return;
            }
           PrintCounter++;
           copier.Print(document);
        }

        #endregion

        #region Scan options
        public void Scan(out IDocument document)
        {
            if (State == IDevice.State.off || copier.GetState() == IDevice.State.off)
            {
                document = new ImageDocument("");
                return;
            }
            ScanCounter++;
            copier.Scan(out document);            
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            if (State == IDevice.State.off)
            {
                document = new ImageDocument("null");
                return;
            }
            ScanCounter++;
            copier.Scan(out document, formatType);
        }

        #endregion

        #region ScanAndPrint
        public void ScanAndPrint()
        {
            if (copier.GetState() == IDevice.State.off || State == IDevice.State.off)
                return;

            ScanCounter++;
            PrintCounter++;
            copier.ScanAndPrint();            
        }
        #endregion


    }
}
