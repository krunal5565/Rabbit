using System;

namespace RabbitApplication.Entity
{
    public class JobProfile
    {
        public long Id { get; set; }
        public string JobProfileId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long NumberOfPositions { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
