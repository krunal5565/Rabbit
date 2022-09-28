using System;

namespace RabbitApplication.Entity
{
    public class CandidateFile
    {
        public long Id { get; set; }
        public string CandiateFileId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }
        public DateTime Createddate { get; set; }
        public DateTime Updateddate { get; set; }
        public bool IsActive { get; set; }
    }
}
