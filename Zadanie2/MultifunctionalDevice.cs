using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ver1;
using static ver1.IDocument;

namespace Zadanie2
{
    public class MultifunctionalDevice : Copier, IFax
    {

        public int FaxCounter { get; protected set; }


        public MultifunctionalDevice()
        {
            PrintCounter = 0;
            ScanCounter = 0;
            FaxCounter = 0;
            Counter = 0;            
        }

        public void FaxSend(in IDocument document)
        {
            if (State == IDevice.State.off)
            {
                Console.WriteLine("Device is off");
                return;
            }

            FaxCounter++;
            Console.WriteLine($"{DateTime.Now} Sent Fax: {document.GetFileName().ToString()}");
            return;
        }

        public void FaxReceive(out IDocument document, IDocument.FormatType formatType = FormatType.JPG) //Fax is receiving documents and printing them. Printed file is always JPG.
        {
            if (State == IDevice.State.off)
            {
                document = new ImageDocument("");
                return;
            }
            else
            {
                PrintCounter++;
                FaxCounter++;
                document = new ImageDocument($"ImageFax{FaxCounter}.jpg");
                Console.WriteLine($"{DateTime.Now} Received Fax: {document.GetFileName().ToString()}");
            }
        }
    }
}
