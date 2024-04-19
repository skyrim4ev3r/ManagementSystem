using System;

namespace Project.ProjectDataBase.Domain
{
    public class SupervisorEvaluationScore
    {
        public string Id { get; set; }
        public string SupervisorEvaluationId { get; set; }
        public SupervisorEvaluation SupervisorEvaluation { get; set; }

        public string Title { get; set; }
        public int Score { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
