using System;

namespace RabbitApplication.Entity
{
    public class CandidateJobProfileMapping
    {
        public long Id { get; set; }
        public string CandidateJobProfileMappingId { get; set; }
        public Candidate Candidateid { get; set; }
        public JobProfile JobProfile { get; set; }
        public DateTime JobAppliedDate { get; set; }
        public string Description { get; set; }
        public DateTime Createddate { get; set; }
        public DateTime Updateddate { get; set; }
        public bool IsActive { get; set; }
    }
}
