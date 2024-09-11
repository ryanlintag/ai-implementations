namespace PdfReaderBusiness.Documents
{
    public class PropertyMeta
    {
        public string StartLine { get; set; }
        public string EndLine { get; set; }
        public string PropertyName { get; set; }
        public bool HasStartedReading { get; private set; }
    }
}
