using System;
using System.Collections.Generic;

namespace Project.DTOs.DomainDTOs
{
    public class SupervisorEvaluationD
    {
        public string Id { get; set; }
        public string CollegianId { get; set; }
        public string GroupId { get; set; }

        public DateTime CreatedAt { get; set; }
        public ICollection<SupervisorEvaluationScoreD> SupervisorEvaluationScores { get; set; }
    }
}
