using PdfReaderBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;

namespace PdfExtractionWebApp.Models
{
    public sealed class StellisViewModel
    {
        public StellisViewModel()
        {
            Position = new PositionStellisModel();
        }
        public PositionStellisModel Position { get; set; }
        //public CandidatePersonalInformationStellisModel PersonalInformation { get; set; }
        //public BasicProfileStellisModel BasicProfile { get; set; }
        //public EducationQualificationsTrainingStellisModel MyProperty { get; set; }
    }

    public class EducationQualificationsTrainingStellisModel
    {
        public string EducationQualificationTraining { get; set; }
        public string StartDate { get; set; }
        public string GraduationDate { get; set; }
        public string EducationInstitution { get; set; }
        public string FieldOfStudy { get; set; }
        public string EducationDegree { get; set; }

    }

    public class BasicProfileStellisModel
    {
        public List<ComputerSkillStellisModel> ComputerSkills { get; set; }
        public string OtherComputerSkills { get; set; }
        public List<LanguageStellisModel> OfficialWHOLanguages { get; set; }
        public List<LanguageStellisModel> OtherLanguages { get; set; }
        public string AdvanceNotice { get; set; }
        public string DateOfAvailability { get; set; }
        public string AcceptableContractDuration { get; set; }
        public string EmployeeStatus { get; set; }
        public string JobLevel { get; set; }
        public string JobType { get; set; }
        public string Schedule { get; set; }
        public string HasPreviouslyWorkedWHO { get; set; }
        public string ManagedRosters { get; set; }
        public string MedicalStatusForDeployment { get; set; }
    }

    public class LanguageStellisModel
    {
        public string LanguageName { get; set; }
        public string Level { get; set; }
    }

    public class ComputerSkillStellisModel
    {
        public string SkillType { get; set; }
        public string SpecificSkill { get; set; }
        public string SkillLevel { get; set; }
    }

    public class CandidatePersonalInformationStellisModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string InternalCandidate { get; set; }
        public string Title { get; set; }
        public string Gender { get; set; }
        public string CivilStatus { get; set; }
        public string Nationality { get; set; }
        public string WHOGeographicalDistributionList { get; set; }
        public string CityOfBirty { get; set; }
        public string CountryOfBirth { get; set; }
        public string DateOfBirth { get; set; }
        public AddressStellisModel CurrentAddress { get; set; }
        public AddressStellisModel PermanentAddress { get; set; }
        public ContactInformationStellisModel ContactInformation { get; set; }
        public RelocationInformationStellisModel RelocationInformation { get; set; }
        public string SpouseEmployedByUN { get; set; }
        public string HasDependentChildren { get; set; }
    }

    public class RelocationInformationStellisModel
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string ExpectedRelocationUntil { get; set; }
    }

    public class ContactInformationStellisModel
    {
        public string PrimaryContactNumber { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
    }

    public class AddressStellisModel
    {
        public string Address1 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }

    public class PositionStellisModel
    {
        public string CandidateName { get; set; }
        public string JobTitle { get; set; }
        public string Step { get; set; }
        public string LatestSubmissionMedium { get; set; }
        public string Recruiter { get; set; }
        public string Status { get; set; }
        public string Source { get; set; }
        public string HiringManager { get; set; }
        public string CreationDate { get; set; }
        public string SubmissionType { get; set; }
    }
}