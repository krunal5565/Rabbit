using System;

namespace RabbitApplication.Model
{
    public class JobVacancy
    {
        public long id { get; set; }
        public long numberofpositions { get; set; }
        public string description { get; set; }
        public JobProfile jobprofiletype { get; set; }
        public DateTime createddate { get; set; }
        public DateTime updateddate { get; set; }
        public bool isactive { get; set; }
    }
}
