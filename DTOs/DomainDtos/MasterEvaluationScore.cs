using System;

namespace Project.DTOs.DomainDTOs
{
    public class MasterEvaluationScoreD
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int MaxScore { get; set; }
        public int Score { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
