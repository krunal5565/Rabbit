using System;

namespace RabbitApplication.Model
{
    public class CandidateJobProfileMapping
    {
        public long id { get; set; }
        public UserDetails candidateid { get; set; }
        public JobProfile jobprofile { get; set; }
        public string description { get; set; }
        public DateTime createddate { get; set; }
        public DateTime updateddate { get; set; }
        public bool isactive { get; set; }
    }
}
