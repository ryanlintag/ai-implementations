using PdfReaderBusiness;
using PdfReaderBusiness.Documents.Stellis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TextSharpReader
{
    public class PdfDataParser : IPdfDataParser
    {
        private Queue<ReadModelMapper> _data = new Queue<ReadModelMapper>(new List<ReadModelMapper>()
        {
            new ReadModelMapper("Step", "Latest Submission Medium", "Step"),
            new ReadModelMapper("Latest Submission Medium", "Recruiter", "LatestSubmissionMedium"),
            new ReadModelMapper("Recruiter", "Status", "Recruiter"),
            new ReadModelMapper("Status", "Source", "Status"),
            new ReadModelMapper("Source", "Hiring Manager", "Source"),
            new ReadModelMapper("Hiring Manager", "Creation Date", "HiringManager"),
            new ReadModelMapper("Creation Date", "Submission Type", "CreationDate"),
            new ReadModelMapper("Submission Type", "Candidate Name Job Title", "SubmissionType"),
        });
        public StellisModel Parse(List<string> pdfText)
        {
            StellisModel stellisModel = new StellisModel();
            ReadModelMapper node = _data.Dequeue();
            StringBuilder builder = new StringBuilder();
            var isNodeMappingOngoing = false;
            var isNodeMapperStarted = false;
            for (int i = 0; i < pdfText.Count; i++)
            { 
                if(node.Start == pdfText[i])
                {
                    isNodeMapperStarted = true;
                    isNodeMappingOngoing = true;
                    continue;
                }
                if (node.End == pdfText[i])
                {
                    _setStellisData(stellisModel, node.PropertyMap, builder.ToString());
                    isNodeMapperStarted = false;
                    isNodeMappingOngoing = false;
                    builder.Clear();
                    if (_data.Count == 0) break;
                    var noderEnd = node.End;
                    node = _data.Dequeue();
                    if(node.Start == noderEnd)
                    {
                        isNodeMapperStarted = true;
                        isNodeMappingOngoing = true;
                    }
                    continue;
                }
                if (isNodeMapperStarted && isNodeMappingOngoing)
                {
                    builder.Append(pdfText[i]);
                    continue;
                }
            }
            return stellisModel;
        }

        private void _setStellisData(StellisModel stellisModel, string propertyMap, string propertyData)
        {
            switch (propertyMap)
            {
                case "Step":
                    stellisModel.Position.Step = propertyData;
                    break;
                case "LatestSubmissionMedium":
                    stellisModel.Position.LatestSubmissionMedium = propertyData;
                    break;
                case "Recruiter":
                    stellisModel.Position.Recruiter = propertyData;
                    break;
                case "Status":
                    stellisModel.Position.Status = propertyData;
                    break;
                case "Source":
                    stellisModel.Position.Source = propertyData;
                    break;
                case "HiringManager":
                    stellisModel.Position.HiringManager = propertyData;
                    break;
                case "CreationDate":
                    stellisModel.Position.CreationDate = propertyData;
                    break;
                case "SubmissionType":
                    stellisModel.Position.SubmissionType = propertyData;
                    break;
                default: break;
            }
        }
    }

    public sealed class ReadModelMapper
    {
        public string Start { get; private set; }

        public string End { get; private set; }
        public string PropertyMap { get; private set; }

        public ReadModelMapper(string start, string end, string propertyMap)
        {
            Start = start;
            End = end;
            PropertyMap = propertyMap;
        }

    }
}
