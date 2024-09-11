using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using PdfReaderBusiness;
using PdfReaderBusiness.Documents.Stellis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TextSharpReader
{
    public class ITextSharpPdfReader : IPdfReader
    {

        public List<string> Read(Stream pdfStream)
        {
            var sb = new List<string>();
            using (PdfReader reader = new PdfReader(pdfStream))
            using (PdfDocument pdfDoc = new PdfDocument(reader))
            {
                for (int page = 1; page <= pdfDoc.GetNumberOfPages(); page++)
                {
                    var strategy = new SimpleTextExtractionStrategy();
                    //the reader reads and scans the page for texts
                    var text = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(page), strategy);
                    //split the text read per line and add to the list
                    sb.AddRange(text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries));
                }
            }
            return sb;
        }
    }
}
