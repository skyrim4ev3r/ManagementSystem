using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.DTOs.DomainDTOs
{
    public class GroupD
    {
        public string Id { get; set; }
        public string InternshipLocationId { get; set; }
        public InternshipLocationD InternshipLocation { get; set; }
        public string MasterId { get; set; }
        public MasterD Master { get; set; }
        public string FieldId { get; set; }
        public FieldD Field { get; set; }
        public string TermId { get; set; }
        public TermD Term { get; set; }
        public string UniversityId { get; set; }
        public UniversityD University { get; set; }
        public string TermName { get; set; }
        public int CountOfCollegians { get; set; }
    }
}
