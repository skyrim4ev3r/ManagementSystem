using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.DTOs.DomainDTOs
{
    public class MasterD
    {
        public string Id { get; set; }
        //public AppUserD User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalNumber { get; set; }
        public string DegreeCode { get; set; }
        public string CourseCode { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
