using System;
using System.Collections.Generic;

namespace Project.ProjectDataBase.Domain
{
    public class MasterEvaluation
    {
        public string Id { get; set; }
        public string CollegianId { get; set; }
        public string Title { get; set; }
        public string GroupId { get; set; }
        public DateTime CreatedAt { get; set; }
        public CollegianGroup CollegianGroup { get; set; }
        public ICollection<MasterEvaluationScore> MasterEvaluationScores { get; set; }
    }
}
