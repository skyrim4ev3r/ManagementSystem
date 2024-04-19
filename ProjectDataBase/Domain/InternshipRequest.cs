using System;

namespace Project.ProjectDataBase.Domain
{
    public class InternshipRequest
    {
        public string Id { get; set; }
        public string CollegianId { get; set; }
        public Collegian Collegian { get; set; }
        public string TermId { get; set; }
        public Term Term { get; set; } 
        public string FieldId { get; set; }
        public Field Field { get; set; }

        public string InternshipLocationName { get; set; }
        public string InternshipLocationAddress { get; set; }
        public string InternshipLocationTel { get; set; }
        public string InternshipLocationSubject { get; set; }
        public string Days { get; set; }
        public DateTime CreatedAt { get; set; }
        public string InternshipSupervisorName { get; set; }
    }
}
