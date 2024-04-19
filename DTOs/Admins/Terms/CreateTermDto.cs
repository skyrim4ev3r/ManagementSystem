using System;

namespace Project.DTOs.Admins.Terms
{
    public class CreateTermDto
    {
        public string Title { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime FinishAt { get; set; }
    }
}
