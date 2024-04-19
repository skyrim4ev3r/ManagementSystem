using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.DTOs.DomainDTOs
{
    public class InternshipLocationD
    {
        public string Id { get; set; }
        //public AppUserD User { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Specialty { get; set; }
        public string Address { get; set; }
        public string CEOFullName { get; set; }
        public string CEOPhoneNumber { get; set; }
        public string TelNumber { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
