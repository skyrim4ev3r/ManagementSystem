using System;

namespace Project.DTOs.Admins.Terms
{
    public class EditTermDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime FinishAt { get; set; }
    }
}
