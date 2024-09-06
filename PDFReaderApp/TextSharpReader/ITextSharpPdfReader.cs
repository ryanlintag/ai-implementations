using PdfReaderBusiness;
using System.IO;
using System.Text;

namespace TextSharpReader
{
    public class ITextSharpPdfReader : IPdfReader
    {
        public StringBuilder Read(byte[] pdfBytes)
        {
            var sb = new StringBuilder();
            return sb;
        }

        public StringBuilder Read(Stream pdfStream)
        {
            var sb = new StringBuilder();
            return sb;
        }
    }
}
