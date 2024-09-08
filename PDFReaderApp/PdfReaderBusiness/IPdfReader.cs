using System.IO;
using System.Text;

namespace PdfReaderBusiness
{
    public interface IPdfReader
    {
        StringBuilder Read(Stream pdfStream);
    }
}
