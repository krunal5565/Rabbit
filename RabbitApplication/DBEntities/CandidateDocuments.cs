using System;

namespace RabbitApplication.Entity
{
    public class CandidateDocuments
    {
        public long Id { get; set; }
        public string CandidateDocumentId { get; set; }
        public string DocumentType { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public DateTime createddate { get; set; }
        public DateTime updateddate { get; set; }
        public bool isactive { get; set; }
    }
}
