using PdfReaderBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TextSharpReader;

namespace PdfExtractionWebApp.Controllers
{
    public class HomeController : Controller
    {
        private IPdfReader _pdfReader { get; set; }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Index(HttpPostedFileBase pdfFile)
        {
            var stream = pdfFile.InputStream;
            var pdfBytes = new byte[stream.Length];
            _pdfReader = new ITextSharpPdfReader();
            var sb = _pdfReader.Read(pdfBytes);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}