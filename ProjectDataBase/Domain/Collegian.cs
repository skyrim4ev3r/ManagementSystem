using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.ProjectDataBase.Domain
{
    public class Collegian
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string CityId { get; set; }
        public City City { get; set; }
        public string Address { get; set; }
        public string NationalNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string TelNumber { get; set; }
        public string UniversityCode { get; set; }
        public string EducationCode { get; set; }
        public char Degree { get; set; }
        public char Nobat { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<CollegianGroup> CollegianGroups { get; set; }
        public virtual ICollection<InternshipRequest> InternshipRequests { get; set; }
    }
}
