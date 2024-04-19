using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.ProjectDataBase.Domain
{
    public class InternshipLocation
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Specialty { get; set; }
        public string Address { get; set; }
        public string CEOFullName { get; set; }
        public string CEOPhoneNumber { get; set; }
        public string TelNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
}
