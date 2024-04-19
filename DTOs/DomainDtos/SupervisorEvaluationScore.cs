using System;

namespace Project.DTOs.DomainDTOs
{
    public class SupervisorEvaluationScoreD
    {
        public string Id { get; set; }
        public string SupervisorEvaluationId { get; set; }

        public string Title { get; set; }
        public int Score { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
