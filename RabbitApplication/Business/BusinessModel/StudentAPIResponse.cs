using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundaClearApp.Business.BusinessModel
{
    public class StudentAPIResponse
    {
            [JsonProperty("address")]
            public string Address { get; set; }
            public string DateOfBirth { get; set; }

            [JsonProperty("gender")]
            public string Gender { get; set; }
            public string MotherName { get; set; }
            public string FatherName { get; set; }
            public string MobileNumber { get; set; }
            public string EmailID { get; set; }
            public string RollNumber { get; set; }
            public string RegistrationNumber { get; set; }
            public string ClassName { get; set; }
            public string AdmissionDate { get; set; }
            public string StreamName { get; set; }
            public string SectionName { get; set; }
            public string StudentId { get; set; }
            public string StudentName { get; set; }
        }
}
