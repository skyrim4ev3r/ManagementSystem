using System;

namespace Project.ProjectDataBase.Domain
{
    public class Attendance
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? FinishAt { get; set; }

        public string CollegianId { get; set; }
        public string GroupId { get; set; }
        public CollegianGroup CollegianGroup { get; set; }
    }
}
