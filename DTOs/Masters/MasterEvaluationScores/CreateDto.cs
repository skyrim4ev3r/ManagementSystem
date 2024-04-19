namespace Project.DTOs.Masters.MasterEvaluationScores
{
    public class CreateDto
    {
        public string MasterEvaluationId { get; set; }
        public string Title { get; set; }
        public int MaxScore { get; set; }
        public int Score { get; set; }
    }
}
