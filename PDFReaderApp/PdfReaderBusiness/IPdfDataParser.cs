using PdfReaderBusiness.Documents.Stellis;
using System.Collections.Generic;

namespace PdfReaderBusiness
{
    public interface IPdfDataParser
    {
        StellisModel Parse(List<string> pdfText);
    }
}
