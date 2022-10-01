using System;

namespace RabbitApplication.Entity
{
    public class CandidateJobProfileMapping
    {
        public long Id { get; set; }
        public string CandidateJobProfileMappingId { get; set; }
        public string Candidateid { get; set; }
        public string JobProfileId { get; set; }
        public string Status { get; set; }
        public string AknoNumber { get; set; }
        public DateTime JobAppliedDate { get; set; }
        public string Description { get; set; }
        public DateTime Createddate { get; set; }
        public DateTime Updateddate { get; set; }
        public bool IsActive { get; set; }
    }
}
