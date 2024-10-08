﻿using iText.Barcodes.Qrcode;
using PdfReaderBusiness;
using PdfReaderBusiness.Documents.Stellis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TextSharpReader
{
    public class PdfDataParser : IPdfDataParser
    {
        private const string _educationQualificationTrainingPropertyMap = "EducationQualificationTranining";
        private const string _workEperiencePropertyMap = "WorkExperienceList";
        private Queue<ReadModelMapper> _data = new Queue<ReadModelMapper>(new List<ReadModelMapper>()
        {
            #region Position
            new ReadModelMapper("Step", "Latest Submission Medium", "Step"),
            new ReadModelMapper("Latest Submission Medium", "Recruiter", "LatestSubmissionMedium"),
            new ReadModelMapper("Recruiter", "Status", "Recruiter"),
            new ReadModelMapper("Status", "Source", "Status"),
            new ReadModelMapper("Source", "Hiring Manager", "Source"),
            new ReadModelMapper("Hiring Manager", "Creation Date", "HiringManager"),
            new ReadModelMapper("Creation Date", "Submission Type", "CreationDate"),
            new ReadModelMapper("Submission Type", "Candidate Name Job Title", "SubmissionType"),
            #endregion

            #region Personal Information
            new ReadModelMapper("First Name", "Family/Last Name", "CandidateFirstName"),
            new ReadModelMapper("Family/Last Name", "Internal Candidate", "CandidateLastName"),
            new ReadModelMapper("Title", "Gender", "CandidateTitle"),
            new ReadModelMapper("Gender", "Civil status", "CandidateGender"),
            new ReadModelMapper("Civil status", "Nationality", "CandidateCivilStatus"),
            new ReadModelMapper("Nationality", "WHO geographical distribution", "CandidateNationality"),
            new ReadModelMapper("WHO geographical distribution", "list", string.Empty),
            new ReadModelMapper("list", "City of birth", "CandidateGeoDistribution"),
            new ReadModelMapper("City of birth", "Country of birth", "CandidateCityOfBirth"),
            new ReadModelMapper("Country of birth", "Date of Birth", "CandidateCountryOfBirth"),
            new ReadModelMapper("Date of Birth", "Current Address", "CandidateDateOfBirth"),
            new ReadModelMapper("Address (line 1)", "City", "CandidateAddressLine1"),
            new ReadModelMapper("City", "Zip/Postal Code", "CandidateCity"),
            new ReadModelMapper("Zip/Postal Code", "Country", "CandidateZip"),
            new ReadModelMapper("Country", "Permanent Address", "CandidateCountry"),
            new ReadModelMapper("Primary contact number", "Mobile number", "CandidatePrimaryContactNumber"),
            new ReadModelMapper("Mobile number", "Email Address", "CandidateMobile"),
            new ReadModelMapper("Email Address", "Relocation Information", "CandidateEmailAddress"),
            new ReadModelMapper("Relocation country", "Relocation city", "CandidateRelocationCountry"),
            new ReadModelMapper("Relocation city", "Expected relocation until", "CandidateRelocationCity"),
            new ReadModelMapper("Expected relocation until", "Employee Data", "CandidateRelocationUntil"),
            new ReadModelMapper("one of its specialized agencies?", "Do you have dependent children?", "CandidateHasSpouseInUN"),
            new ReadModelMapper("Do you have dependent children?", "Basic Profile", "CandidateHasDependentChildren"),
            #endregion

            #region Basic Profile
            new ReadModelMapper("Computer skills/applications", "Other Computer Skills", "BasicProfileComputerSkills"),
            new ReadModelMapper("Official WHO languages", "Other Languages", "BasicProfileOfficialLanguages"),
            new ReadModelMapper("Other languages", "Basic Profile", "BasicProfileOtherLanguages"),
            new ReadModelMapper("Advance Notice", "Date of Availability", "BasicProfileAdvanceNotice"),
            new ReadModelMapper("Date of Availability", "Please select the contract durations you", "BasicProfileDateOfAvailability"),
            new ReadModelMapper("would be willing to accept", "Employee Status", "BasicProfileMinContractDuration"),
            new ReadModelMapper("Employee Status", "Job Level", "BasicProfileEmployeeStatus"),
            new ReadModelMapper("Job Level", "Job Type", "BasicProfileJobLevel"),
            new ReadModelMapper("Job Type", "Schedule", "BasicProfileJobType"),
            new ReadModelMapper("Schedule", "Have you previously worked with WHO", "BasicProfileSchedule"),
            new ReadModelMapper("Have you previously worked with WHO", "WHO Rosters", "BasicProfilePreviouslyWorkedWithWHO"),
            new ReadModelMapper("Managed rosters", "Med. status for deployment", "BasicProfileManagedRosters"),
            new ReadModelMapper("Med. status for deployment", "WHO Internships", "BasicProfileMedicalStatusForDeployment"),
            #endregion


            new ReadModelMapper("Education, Qualification and Training", "Work Experience", _educationQualificationTrainingPropertyMap),
            new ReadModelMapper("Work Experience", "Cover Letter", _workEperiencePropertyMap),

        });
        private Queue<ReadModelMapper> educationQueue = new Queue<ReadModelMapper>();
        private Queue<string> _educTrainQualify { get; set; } = new Queue<string>();
        private Queue<string> _workExperience { get; set; } = new Queue<string>();
        private readonly List<ReadModelMapper> _workExperienceModelMapperList = 
            new List<ReadModelMapper>()
            {
                new ReadModelMapper("Job Title", "Work schedule", "WEJobTitle"),
                new ReadModelMapper("Work schedule", "Start Date", "WEWorkSchedule"),
                new ReadModelMapper("Start Date", "Type of business", "WEStartDate"),
                new ReadModelMapper("Type of business", "Supervisor's Name", "WETypeOfBusiness"),
                new ReadModelMapper("Supervisor's Name", "Supervisor's Title", "WESupervisorName"),
                new ReadModelMapper("Supervisor's Title", "Area of Work", "WESupervisorTitle"),
                new ReadModelMapper("Area of Work", "Duties and responsibilities", "WEAreaOfWork"),
                new ReadModelMapper("Duties and responsibilities", "Key Achievements", "WEDutiesAndResponsibilities"),
                new ReadModelMapper("Key Achievements", "Reference", "WEKeyAchivements"),
            };
        public StellisModel Parse(List<string> pdfText)
        {
            StellisModel stellisModel = new StellisModel();
            ReadModelMapper node = _data.Dequeue();
            StringBuilder builder = new StringBuilder();
            var isNodeMappingOngoing = false;
            var isNodeMapperStarted = false;
            var isEducationQualificationTraining = false;
            for (int i = 0; i < pdfText.Count; i++)
            { 
                if(node.Start == pdfText[i])
                {
                    isNodeMapperStarted = true;
                    isNodeMappingOngoing = true;
                    if(node.PropertyMap == _educationQualificationTrainingPropertyMap && isEducationQualificationTraining == false)
                    {
                        isEducationQualificationTraining = true;
                    }
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
                    if (node.PropertyMap == _educationQualificationTrainingPropertyMap)
                    {
                        isEducationQualificationTraining = false;
                    }
                    continue;
                }
                if (isNodeMapperStarted && isNodeMappingOngoing)
                {
                    builder.Append(pdfText[i]);
                    if(node.PropertyMap == _educationQualificationTrainingPropertyMap)
                    {
                        _educTrainQualify.Enqueue(pdfText[i]);
                    }
                    if (node.PropertyMap == _workEperiencePropertyMap)
                    {
                        _workExperience.Enqueue(pdfText[i]);
                    }
                    continue;
                }
            }
            return stellisModel;
        }

        private void _setStellisData(StellisModel stellisModel, string propertyMap, string propertyData)
        {
            switch (propertyMap)
            {
                #region stellisModel.Position
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
                #endregion
                #region stellisModel.PersonalInfo
                case "CandidateFirstName":
                    stellisModel.PersonalInfo.FirstName = propertyData;
                    break;
                case "CandidateLastName":
                    stellisModel.PersonalInfo.LastName = propertyData;
                    break;
                case "CandidateTitle":
                    stellisModel.PersonalInfo.Title = propertyData;
                    break;
                case "CandidateGender":
                    stellisModel.PersonalInfo.Gender = propertyData;
                    break;
                case "CandidateCivilStatus":
                    stellisModel.PersonalInfo.CivilStatus = propertyData;
                    break;
                case "CandidateNationality":
                    stellisModel.PersonalInfo.Nationality = propertyData;
                    break;
                case "CandidateGeoDistribution":
                    stellisModel.PersonalInfo.GeoDistribution = propertyData;
                    break;
                case "CandidateCityOfBirth":
                    stellisModel.PersonalInfo.CityOfBirth = propertyData;
                    break;
                case "CandidateCountryOfBirth":
                    stellisModel.PersonalInfo.CountryOfBirth = propertyData;
                    break;
                case "CandidateDateOfBirth":
                    stellisModel.PersonalInfo.City = propertyData;
                    break;
                case "CandidateAddressLine1":
                    stellisModel.PersonalInfo.City = propertyData;
                    break;
                case "CandidateCity":
                    stellisModel.PersonalInfo.City = propertyData;
                    break;
                case "CandidateZip":
                    stellisModel.PersonalInfo.ZipCode = propertyData;
                    break;
                case "CandidateCountry":
                    stellisModel.PersonalInfo.Country = propertyData;
                    break;
                case "CandidatePrimaryContactNumber":
                    stellisModel.PersonalInfo.PrimaryContactNumber = propertyData;
                    break;
                case "CandidateEmailAddress":
                    stellisModel.PersonalInfo.EmailAddress = propertyData;
                    break;
                case "CandidateRelocationCountry":
                    stellisModel.PersonalInfo.RelocationCountry = propertyData;
                    break;
                case "CandidateRelocationCity":
                    stellisModel.PersonalInfo.RelocationCity = propertyData;
                    break;
                case "CandidateRelocationUntil":
                    stellisModel.PersonalInfo.RelocationUntil = propertyData;
                    break;
                case "CandidateHasSpouseInUN":
                    stellisModel.PersonalInfo.HasSpouseInUN = propertyData;
                    break;
                case "CandidateHasDependentChildren":
                    stellisModel.PersonalInfo.HasDependentChildren = propertyData;
                    break;
                #endregion
                #region stellisModel.BasicProfile
                case "BasicProfileComputerSkills":
                    var computerSkills = propertyData.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    var lstCompSkills = new List<ComputerSkill>();
                    foreach(var computerSkill in computerSkills)
                    {
                        var cs = computerSkill.Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries);
                        if(cs.Length > 2)
                        {
                            lstCompSkills.Add(new ComputerSkill()
                            {
                                Category = cs[0].Trim(),
                                AppName = cs[1].Trim(),
                                Proficiency = cs[2].Trim()
                            });
                        }
                    }
                    stellisModel.BasicInfo.ComputerSkills.AddRange(lstCompSkills);
                    break;
                case "BasicProfileOfficialLanguages":
                    var officialLanguages = propertyData.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    var lstLanguage = new List<LanguageLevel>();
                    foreach(var language in officialLanguages)
                    {
                        var le = language.Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries);
                        if(le.Length > 1)
                        {
                            lstLanguage.Add(new LanguageLevel()
                            {
                                LanguageName = le[0].Trim(),
                                LanguageProficiency = le[1].Trim()
                            });
                        }
                    }
                    stellisModel.BasicInfo.WHOOfficialLanguages.AddRange(lstLanguage);
                    break;
                case "BasicProfileOtherLanguages":
                    var otherLanguages = propertyData.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    var lstOLanguage = new List<LanguageLevel>();
                    foreach (var language in otherLanguages)
                    {
                        var le = language.Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries);
                        if (le.Length > 1)
                        {
                            lstOLanguage.Add(new LanguageLevel()
                            {
                                LanguageName = le[0].Trim(),
                                LanguageProficiency = le[1].Trim()
                            });
                        }
                    }
                    stellisModel.BasicInfo.OtherLanguages.AddRange(lstOLanguage);
                break;
                case "BasicProfileAdvanceNotice":
                    stellisModel.BasicInfo.AdvanceNotice = propertyData;
                break;
                case "BasicProfileDateOfAvailability":
                    stellisModel.BasicInfo.DateOfAvailability = propertyData;
                break;
                case "BasicProfileMinContractDuration":
                    stellisModel.BasicInfo.MinContractDuration = propertyData;
                break;
                case "BasicProfileEmployeeStatus":
                    stellisModel.BasicInfo.EmployeeStatus = propertyData;
                break;
                case "BasicProfileJobLevel":
                    stellisModel.BasicInfo.JobLevel = propertyData;
                break;
                case "BasicProfileJobType":
                    stellisModel.BasicInfo.JobType = propertyData;
                break;
                case "BasicProfileSchedule":
                    stellisModel.BasicInfo.Schedule = propertyData;
                break;
                case "BasicProfilePreviouslyWorkedWithWHO":
                    stellisModel.BasicInfo.PreviouslyWorkedWithWHO = propertyData;
                break;
                case "BasicProfileMedicalStatusForDeployment":
                    stellisModel.BasicInfo.MedicalStatusForDeployment = propertyData;
                break;

                #endregion
                case _educationQualificationTrainingPropertyMap:
                    buildEductionTrainingQualificationList(stellisModel);
                    break;
                case _workEperiencePropertyMap:
                    buildWorkExperienceList(stellisModel);
                    break;
                default: break;
            }
        }

        private void buildWorkExperienceList(StellisModel stellisModel)
        {
            ReadModelMapper[] workExperienceQueue = _workExperienceModelMapperList.ToArray();
            var list = new List<WorkExperienceStellisModel>();
            //1. While the _workExperienceQueue has data loop through the list
            while (_workExperience.Any())
            {
                var we = new WorkExperienceStellisModel();

                var isReadingData = false;
                StringBuilder sb = new StringBuilder();
                string mapperEnd = string.Empty;
                for(int i = 0; i < workExperienceQueue.Length; i++)
                {
                    var mapper = workExperienceQueue[i];
                    while (_workExperience.Any()) 
                    {
                        var currentString = _workExperience.Dequeue();
                        if (mapper.Start == currentString && isReadingData == false)
                        {
                            isReadingData = true;
                            sb.Clear();
                            continue;
                        }
                        if (mapper.End == currentString)
                        {
                            setWorkExperienceProperty(we, sb.ToString(), mapper.PropertyMap);
                            isReadingData = false;
                            if(i < workExperienceQueue.Length - 1)
                            {
                                if(mapper.End == workExperienceQueue[i + 1].Start)
                                {
                                    isReadingData = true;
                                    sb.Clear();
                                }
                            }
                            break;
                        }
                        sb.Append($"{currentString} ");
                    }

                }
                list.Add(we);
            }
            if(list.Count > 0)
            {
                stellisModel.WorkExperiences.AddRange(list);
            }
        }

        private void setWorkExperienceProperty(WorkExperienceStellisModel we, string value, string propertyMap)
        {
            switch (propertyMap) 
            {
                case "WEJobTitle":
                    we.JobTitle = value;
                    break;
                case "WEWorkSchedule":
                    we.WorkSchedule = value;
                    break;
                case "WEStartDate":
                    we.StartDate = value;
                    break;
                case "WETypeOfBusiness":
                    we.TypeOfBusiness = value;
                    break;
                case "WEMajorOffice":
                    we.MajorOffice = value;
                    break;
                case "WEDepartmentDivision":
                    we.DepartmentDivision = value;
                    break;
                case "WEDutyStation":
                    we.DutyStation = value;
                    break;
                case "WEContractType":
                    we.ContractType = value;
                    break;
                case "WEGrade":
                    we.Grade = value;
                    break;
                case "WESupervisorName":
                    we.SupervisorName = value;
                    break;
                case "WESupervisorTitle":
                    we.SupervisorTitle = value;
                    break;
                //case "WESupervisorPhone":
                //    we.SupervisorPhone = value;
                //    break;
                //case "WESupervisorEmail":
                //    we.SupervisorEmail = value;
                //    break;
                case "WEAreaOfWork":
                    we.AreaOfWork = value;
                    break;
                case "WENatureOfWork":
                    we.NatureOfWork = value;
                    break;
                case "WEDutiesAndResponsibilities":
                    we.DutiesAndResponsibilities = value;
                    break;
                case "WEKeyAchivements":
                    we.Achievements = value;
                    break;
                default:
                    break;
            }
        }

        private void buildEductionTrainingQualificationList(StellisModel stellisModel)
        {
            var x = _educTrainQualify.ToArray();

            //var currentLine = _educTrainQualify.Dequeue();
            List<EducationTrainingQualification> list = new List<EducationTrainingQualification>();
            EducationTrainingQualification item = null;
            do
            {
                var currentLine = _educTrainQualify.Dequeue();
                var nextLine = x[x.Length - _educTrainQualify.Count];
                if(currentLine == "Education/Qualification/Training")
                {
                    if(item != null)
                    {
                        list.Add(item);
                    }
                    switch (nextLine)
                    {
                        case "Education":
                            item = new Education(nextLine);
                            break;
                        case "Qualification":
                            item = new Qualification(nextLine);
                            break;
                        case "Training":
                            item = new Training(nextLine);
                            break;
                        default:
                            break;
                    }
                    _educTrainQualify.Dequeue();
                    continue;
                }
                //map the data
                if(item != null)
                {
                    item.AddString(currentLine);
                }

                if (_educTrainQualify.Count == 1)
                {
                    //currentline will be the last line
                    currentLine = _educTrainQualify.Dequeue();
                    item.AddString(currentLine);
                    list.Add(item);
                    break;
                }
            } while (_educTrainQualify.Any());
            if(list != null && list.Count > 0)
            {
                stellisModel.EducationTrainingQualifications.AddRange(list);
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
