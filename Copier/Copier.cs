using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ver1
{
    public class Copier : IPrinter, IScanner
    {
        public int PrintCounter { get; init; }

        public int ScanCounter { get; init; }

        public int Counter { get; init; }

        public Copier()
        {
            this.PrintCounter = 0;
            this.Counter = 0;
            this.ScanCounter = 0;
        }

        public IDevice.State GetState()
        {
            if( (((IScanner)this).GetState() == IDevice.State.off))
                return IDevice.State.off;
            if ((((IPrinter)this).GetState() == IDevice.State.off))
                return IDevice.State.off;
            return IDevice.State.on;
        }


        public void PowerOff()
        {
            //IScanner.State = IDevice.State.off;
            Console.WriteLine("Device is off ...");
        }

        public void PowerOn()
        {
            throw new NotImplementedException();
        }

        public void Print(in IDocument document)
        {
            throw new NotImplementedException();
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            throw new NotImplementedException();
        }
    }
}
