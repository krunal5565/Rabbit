using System;
using System.Collections.Generic;

namespace RabbitApplication.Models
{
    public class JobApplySuccessModel
    {
        public string CandidateId { get; set; }
        public string JobApplicationDate { get; set; }
        public string AkNo { get; set; }
        public string JobProfileName { get; set; }
    }

    public class MyJobApplications
    {
        public string CandidateId { get; set; }
        public string JobApplicationDate { get; set; }
        public string AkNo { get; set; }
        public string CandidateJobProfileMappingId { get; set; }
        public string Status { get; set; }
        public string JobProfileName { get; set; }
    }

    public class Dropdown
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

    public class CandidateModel
    {
        public long Id { get; set; }
        public string CandidateId { get; set; }
        public string JobProfileId { get; set; }
        public string JobProfileName { get; set; }
        public string Title { get; set; }
        public string Fname { get; set; }
        public string Mname { get; set; }
        public string Lname { get; set; }
        public IEnumerable<Dropdown> Gender { get; set; }
        public string Caste { get; set; }
        public DateTime? DOB { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string AlternateMobile { get; set; }
        public DateTime Createddate { get; set; }
        public DateTime Updateddate { get; set; }
        public bool IsActive { get; set; }
        public string CandidateJobProfileMappingId { get; set; }

        public List<CandidateFileModel> Files { get; set; }
        public List<EducationalDetailsModel> EducationalDetails { get; set; }
    }

    public class EducationalDetailsModel
    {
        public long id { get; set; }
        public string CandidateId { get; set; }
        public string Qualification { get; set; }
        public string YearOfPassing { get; set; }
        public string Percentage { get; set; }
        public string Board { get; set; }
        public string description { get; set; }
        public DateTime createddate { get; set; }
        public DateTime updateddate { get; set; }
        public bool isactive { get; set; }
    }
}
