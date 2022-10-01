using System;
using System.ComponentModel.DataAnnotations;

namespace RabbitApplication.Models
{
    public enum Gender
    {
        Male,
        Female
    }

    public enum Caste
    {
        General,
        SEBC,
        SC,
        ST,
        OBC
    }

    public enum Title
    {
        Mr,
        Mrs,
        Miss,
        Dr,
        ER
    }

    public enum DocumentType
    {
        Photo,
        EducationalCertificate,
        ExperienceCertificate,
        IDProof,
        AddressProof
    }
}
