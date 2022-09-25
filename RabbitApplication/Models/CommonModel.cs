using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundaClearApp.Business
{
    public class UserModel
    {
        public long id { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string sessionid { get; set; }
        public DateTime createddate { get; set; }
        public DateTime updateddate { get; set; }
        public bool isactive { get; set; }
    }

    public class CommonModel
    {
        public string Name { get; set; }
        public string YOP { get; set; }
        public string ISBN { get; set; }
        public string Quantity { get; set; }
        public string Supplier { get; set; }
    }
}


public class StudentModel
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
}

public class SupplierModel
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string MobileNumber { get; set; }
}
