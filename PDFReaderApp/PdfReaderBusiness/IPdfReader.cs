using System.IO;
using System.Text;

namespace PdfReaderBusiness
{
    public interface IPdfReader
    {
        StringBuilder Read(byte[] pdfBytes);
        StringBuilder Read(Stream pdfStream);
    }
}
