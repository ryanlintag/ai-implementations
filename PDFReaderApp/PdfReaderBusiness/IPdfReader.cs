using PdfReaderBusiness.Documents.Stellis;
using System.Collections.Generic;
using System.IO;

namespace PdfReaderBusiness
{
    public interface IPdfReader
    {
        List<string> Read(Stream pdfStream);
    }

}
