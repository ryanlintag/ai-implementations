using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using PdfReaderBusiness;
using System.IO;
using System.Text;

namespace TextSharpReader
{
    public class ITextSharpPdfReader : IPdfReader
    {

        public StringBuilder Read(Stream pdfStream)
        {
            var sb = new StringBuilder();
            using (PdfReader reader = new PdfReader(pdfStream))
            using (PdfDocument pdfDoc = new PdfDocument(reader))
            {
                for (int page = 1; page <= pdfDoc.GetNumberOfPages(); page++)
                {
                    var strategy = new SimpleTextExtractionStrategy();
                    var text = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(page), strategy);
                    sb.AppendLine(text);
                }
            }
            return sb;
        }
    }
}
