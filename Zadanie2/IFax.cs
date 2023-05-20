using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ver1;
using static ver1.IDocument;

namespace Zadanie2;

public interface IFax : IDevice
{

     void FaxSend(in IDocument document);
    public void FaxReceive(out IDocument document, IDocument.FormatType formatType);

}
