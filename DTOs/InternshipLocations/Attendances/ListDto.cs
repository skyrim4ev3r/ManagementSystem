namespace Project.DTOs.InternshipLocations.Attendances
{
    public class ListDto
    {
        public string CollegianId { get; set; }
        public string GroupId { get; set; }
    }

    public class ChangeStateDto
    {
        public string CollegianId { get; set; }
        public string GroupId { get; set; }
        //public string CurrentState { get; set; }
    }
}
