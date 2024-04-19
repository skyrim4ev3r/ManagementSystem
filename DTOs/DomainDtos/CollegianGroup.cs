using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.DTOs.DomainDTOs
{
    public class CollegianGroupD
    {
        public string CollegianId { get; set; }
        public CollegianD Collegian { get; set; }

        public string GroupId { get; set; }
        public GroupD Group { get; set; }

        public ICollection<MasterEvaluationD> MasterEvaluations { get; set; }
        public ICollection<SupervisorEvaluationD> SupervisorEvaluations { get; set; }

        public DateTime? StartAt { get; set; }
        public DateTime? FinishAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public int InternshipLocationScoreByMaster { get; set; }
        public int InternshipLocationScoreByCollegian { get; set; }

    }
}
