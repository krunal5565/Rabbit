using System;

namespace RabbitApplication.Entity
{
    public class LoginDetails
    {
        public long Id { get; set; }
        public string LoginDetailsId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public long? CandidateId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
