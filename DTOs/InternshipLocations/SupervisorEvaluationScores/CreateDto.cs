namespace Project.DTOs.InternshipLocations.SupervisorEvaluationScores
{
    public class CreateDto
    {
        public string SupervisorEvaluationId { get; set; }
        public string Title { get; set; }
        public int Score { get; set; }
        public string Status { get; set; }
    }
}
