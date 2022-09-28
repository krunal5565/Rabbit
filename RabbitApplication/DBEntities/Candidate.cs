using System;

namespace RabbitApplication.Entity
{
    public class Candidate
    {
        public long Id { get; set; }
        public string CandidateId { get; set; }
        public string Title { get; set; }
        public string Fname { get; set; }
        public string Mname { get; set; }
        public string Lname { get; set; }
        public string Gender { get; set; }
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
    }
}
