using RabbitApplication.Entity;
using RabbitApplication.Models;

namespace RabbitApplication.Helpers
{
    public class ApplicationHelper
    {
        public static EducationalDetails BindEducationDetailsModelToEntity(EducationalDetailsModel model)
        {
            EducationalDetails educationalDetails = new EducationalDetails();

            if(model != null)
            {
                educationalDetails.Qualification = model.Qualification;
                educationalDetails.Board = model.Board;
                educationalDetails.YearOfPassing = model.YearOfPassing;
                educationalDetails.Percentage = model.Percentage;
            }

            return educationalDetails;
        }

        public static JobProfile BindJobProfileModelToEntity(JobProfileModel jobProfileModel)
        {
            JobProfile jobProfile = new JobProfile();

            if(jobProfileModel != null)
            {
                jobProfile.JobProfileId = jobProfileModel.JobProfileId;
                jobProfile.NumberOfPositions = jobProfileModel.NumberOfPositions;
                jobProfile.EndDate = jobProfileModel.EndDate;
                jobProfile.StartDate = jobProfileModel.StartDate;
                jobProfile.Description = jobProfileModel.Description;
                jobProfile.Id = jobProfileModel.Id;
                jobProfile.Name = jobProfileModel.Name;
            }

            return jobProfile; 
        }

        public static JobProfileModel BindJobProfileEntityToModel(JobProfile jobProfile)
        {
            JobProfileModel objJobProfileData = new JobProfileModel();

            if(jobProfile != null)
            {
                objJobProfileData.JobProfileId = jobProfile.JobProfileId;
                objJobProfileData.NumberOfPositions = jobProfile.NumberOfPositions;
                objJobProfileData.EndDate = jobProfile.EndDate;
                objJobProfileData.Description = jobProfile.Description;
                objJobProfileData.ShortDescription = jobProfile.Description != null && jobProfile.Description.Length> 20 ? jobProfile.Description.Substring(0,20): jobProfile.Description;
                objJobProfileData.StartDate = jobProfile.StartDate;
                objJobProfileData.Name = jobProfile.Name;
            }

            return objJobProfileData;
        }

        public static CandidateModel BindCandidateHelperData(Candidate candidate)
        {
            CandidateModel objCandidateModel = new CandidateModel();

            if (candidate != null)
            {
                objCandidateModel.AlternateMobile = candidate.AlternateMobile;
                objCandidateModel.Fname = candidate.Fname;
                objCandidateModel.Lname = candidate.Lname;
                objCandidateModel.Mname = candidate.Mname;
                objCandidateModel.CandidateId = candidate.CandidateId;
                objCandidateModel.Mobile = candidate.Mobile;
                objCandidateModel.IsActive = candidate.IsActive;
                objCandidateModel.Id = candidate.Id;
                objCandidateModel.Gender = candidate.Gender;
                objCandidateModel.Email = candidate.Email;
                objCandidateModel.City = candidate.City;
                objCandidateModel.Caste = candidate.Caste;
                objCandidateModel.PermanentAddress = candidate.PermanentAddress;
                objCandidateModel.Pincode = candidate.Pincode;
                objCandidateModel.PresentAddress = candidate.PresentAddress;
                objCandidateModel.Title = candidate.Title;
                objCandidateModel.DOB = candidate.DOB;
                objCandidateModel.Username = candidate.Email;
            }

            return objCandidateModel;
        }


        public static CandidateFileModel BindFileHelperData(CandidateFile candidateFile)
        {
            CandidateFileModel objCandidateFileModel = new CandidateFileModel();

            if (candidateFile != null)
            {
                objCandidateFileModel.Name = candidateFile.Name;
                objCandidateFileModel.Description = candidateFile.Description;
                objCandidateFileModel.FileType = candidateFile.FileType;
                objCandidateFileModel.FilePath = candidateFile.FilePath;
                objCandidateFileModel.Createddate = candidateFile.Createddate;
                objCandidateFileModel.Id = candidateFile.Id;
                objCandidateFileModel.CandiateFileId = candidateFile.CandidateFileId;
            }

            return objCandidateFileModel;
        }
    }
}
