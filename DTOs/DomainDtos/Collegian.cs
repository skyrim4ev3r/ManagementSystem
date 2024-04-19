using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.DTOs.DomainDTOs
{
    public class CollegianD
    {
        public string Id { get; set; }
        //public AppUserD User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string CityId { get; set; }
        public CityD City { get; set; }
        public string Address { get; set; }
        public string NationalNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string TelNumber { get; set; }
        public string UniversityCode { get; set; }
        public string EducationCode { get; set; }
        public DateTime CreatedAt { get; set; }     
    }
}
