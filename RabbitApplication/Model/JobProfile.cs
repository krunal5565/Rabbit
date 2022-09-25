using System;

namespace RabbitApplication.Model
{
    public class JobProfile
    {
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public JobProfileType jobprofiletypeid { get; set; }
        public DateTime createddate { get; set; }
        public DateTime updateddate { get; set; }
        public bool isactive { get; set; }
    }
}
