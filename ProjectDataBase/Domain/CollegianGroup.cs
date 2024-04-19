using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.ProjectDataBase.Domain
{
    public class CollegianGroup
    {
        public string CollegianId { get; set; }
        public Collegian Collegian { get; set; }

        public string GroupId { get; set; }
        public Group Group { get; set; }

        public ICollection<MasterEvaluation> MasterEvaluations { get; set; }
        public ICollection<SupervisorEvaluation> SupervisorEvaluations { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<CollegianGroupFormField> CollegianGroupFormFields { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? FinishAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public int InternshipLocationScoreByMaster { get; set; }
        public int InternshipLocationScoreByCollegian { get; set; }

    }
}
