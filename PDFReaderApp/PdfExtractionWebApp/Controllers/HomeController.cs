using PdfReaderBusiness;
using PdfReaderBusiness.Documents.Stellis;
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
        private IPdfDataParser _pdfDataParser { get; set; }
        //private readonly List<Tuple<string, string, bool>> _nodes = new List<Tuple<string, string, bool>>()
        //{
        //    new Tuple<string, string, bool>("Step", "Latest Submission Medium", true),
        //    new Tuple<string, string, bool>("Latest Submission Medium", "Recruiter", true),
        //    new Tuple<string, string, bool>("Recruiter", "Status", true),
        //    new Tuple<string, string, bool>("Status", "Source", true),
        //    new Tuple<string, string, bool>("Source", "Hiring Manager", true),
        //    new Tuple<string, string, bool>("Hiring Manager", "Creation Date", true),
        //    new Tuple<string, string, bool>("Creation Date", "Submission Type", true),
        //    new Tuple<string, string, bool>("Submission Type", "Candidate Name Job Title", true),
        //    new Tuple<string, string, bool>("Candidate Name Job Title", "First Name", false),
        //};
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Index(HttpPostedFileBase pdfFile)
        {
            var stream = pdfFile.InputStream;
            _pdfReader = new ITextSharpPdfReader();
            var sb = _pdfReader.Read(stream);
            _pdfDataParser = new PdfDataParser();
            var stellisData = _pdfDataParser.Parse(sb);
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