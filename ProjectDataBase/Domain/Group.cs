using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.ProjectDataBase.Domain
{
    public class Group
    {
        public string Id { get; set; }
        public string InternshipLocationId { get; set; }
        public InternshipLocation InternshipLocation { get; set; }
        public string MasterId { get; set; }        
        public Master Master { get; set; }
        public string FieldId { get; set; }
        public Field Field { get; set; }
        public string TermId { get; set; }
        public Term Term { get; set; }
        public string FormId { get; set; }
        public Form Form { get; set; }
        public string UniversityId { get; set; }
        public University University { get; set; }
        public string TermName { get; set; }

        public virtual ICollection<CollegianGroup> CollegianGroups { get; set; }
    }
}
