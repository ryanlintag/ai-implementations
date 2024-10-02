using System.Collections.Generic;

namespace PdfReaderBusiness.Documents.Stellis
{
    public class StellisModel
    {
        public StellisModel()
        {
            Position = new PositionStellisModel();
            PersonalInfo = new CandidatePersonalInfoStellisModel();
            BasicInfo = new BasicInfoStellisModel();
        }
        public PositionStellisModel Position { get; set; }
        public CandidatePersonalInfoStellisModel PersonalInfo { get; set; }
        public BasicInfoStellisModel BasicInfo { get; set; }
    }

    public class BasicInfoStellisModel
    {
        public BasicInfoStellisModel()
        {
            ComputerSkills = new List<ComputerSkill>();
            OtherComputerSkills = new List<ComputerSkill>();
            WHOOfficialLanguages = new List<LanguageLevel>();
            OtherLanguages = new List<LanguageLevel>();
        }
        public List<ComputerSkill> ComputerSkills { get; set; }
        public List<ComputerSkill> OtherComputerSkills { get; set; }
        public List<LanguageLevel> WHOOfficialLanguages { get; set; }
        public List<LanguageLevel> OtherLanguages { get; set; }
    }

    public class ComputerSkill
    {
        public string Category { get; set; }
        public string AppName { get; set; }
        public string Proficiency { get; set; }
    }

    public class LanguageLevel
    {
        public string LanguageName { get; set; }
        public string LanguageProficiency { get; set; }
    }
}
