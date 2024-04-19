using System;

namespace Project.ProjectDataBase.Domain
{
    public class MasterEvaluationScore
    {
        public string Id { get; set; }
        public string MasterEvaluationId { get; set; }
        public MasterEvaluation MasterEvaluation { get; set; }

        public string Title { get; set; }
        public int MaxScore { get; set; }
        public int Score { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
