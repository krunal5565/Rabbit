using System;

namespace RabbitApplication.Entity
{
	public class EducationalDetails
	{
		public long id { get; set; }
		public string CandidateId { get; set; }
		public string Qualification { get; set; }
		public string YearOfPassing { get; set; }
		public string Percentage { get; set; }
		public string Board { get; set; }
		public string description { get; set; }
		public DateTime  createddate { get; set; }
		public DateTime updateddate { get; set; }
		public bool isactive { get; set; }
	}
}
