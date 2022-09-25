using System;

namespace RabbitApplication.Model
{
    public class UserDetails
    {
        public long id { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string sessionid { get; set; }
        public RoleType roletypeid { get; set; }
        public UserType usertypeid { get; set; }
        public DateTime createddate { get; set; }
        public DateTime updateddate { get; set; }
        public bool isactive { get; set; }
    }
}
