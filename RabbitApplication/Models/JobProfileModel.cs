using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RabbitApplication.Models
{
    public class PersonModel
    {
        [Display(Name = "Personal Details:")]
        [Required(ErrorMessage = "Personal Details is required.")]
        [AllowHtml]
        public string PersonalDetails { get; set; }
    }

    public class JobProfileModel
    {
        public long Id { get; set; }
        public string JobProfileId { get; set; }
        public string Name { get; set; }

        [AllowHtml]
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public long NumberOfPositions { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
