namespace PdfReaderBusiness.Documents.Stellis
{
    public class StellisModel
    {
        public StellisModel()
        {
            Position = new PositionStellisModel();
        }
        public PositionStellisModel Position { get; set; }
    }
}
