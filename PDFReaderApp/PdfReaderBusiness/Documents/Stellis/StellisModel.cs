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
            WorkExperiences = new List<WorkExperienceStellisModel>();
            EducationTrainingQualifications = new List<EducationTrainingQualification>();
        }
        public PositionStellisModel Position { get; set; }
        public CandidatePersonalInfoStellisModel PersonalInfo { get; set; }
        public BasicInfoStellisModel BasicInfo { get; set; }

        public List<EducationTrainingQualification> EducationTrainingQualifications { get; set; }
        public List<WorkExperienceStellisModel> WorkExperiences { get; set; }
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
        public string AdvanceNotice { get; set; }
        public string DateOfAvailability { get; set; }
        public string MinContractDuration { get; set; }
        public string EmployeeStatus { get; set; }
        public string JobLevel { get; set; }
        public string JobType { get; set; }
        public string Schedule { get; set; }
        public string PreviouslyWorkedWithWHO { get; set; }
        public string MedicalStatusForDeployment { get; set; }
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

    public class WorkExperienceStellisModel
    {
        public string JobTitle { get; set; }
        public string WorkSchedule { get; set; }
        public string StartDate { get; set; }
        public string TypeOfBusiness { get; set; }
        public string MajorOffice { get; set; }
        public string DepartmentDivision { get; set; }
        public string DutyStation { get; set; }
        public string ContractType { get; set; }
        public string Grade { get; set; }
        public string SupervisorName { get; set; }
        public string SupervisorTitle { get; set; }
        public string SupervisorPhone { get; set; }
        public string SupervisorEmail { get; set; }
        public string AreaOfWork { get; set; }
        public string NatureOfWork { get; set; }
        public string DutiesAndResponsibilities { get; set; }
        public string Achievements { get; set; }
    }
    public abstract class EducationTrainingQualification
    {
        public string EducationType { get; private set; }

        protected EducationTrainingQualification(string educationType)
        {
            EducationType = educationType;
        }

        public abstract void AddString(string current);
    }
    public sealed class Education : EducationTrainingQualification
    {
        public Education(string educationType) : base(educationType)
        {
        }
        private List<string> items { get; set; } = new List<string>();
        public override void AddString(string current)
        {
            items.Add(current);
        }
    }
    public sealed class Training : EducationTrainingQualification
    {
        public Training(string educationType) : base(educationType)
        {
        }

        private List<string> items { get; set; } = new List<string>();
        public override void AddString(string current)
        {
            items.Add(current);
        }
    }
    public sealed class Qualification : EducationTrainingQualification
    {
        public Qualification(string educationType) : base(educationType)
        {
        }

        private List<string> items { get; set; } = new List<string>();
        public override void AddString(string current)
        {
            items.Add(current);
        }
    }
}
