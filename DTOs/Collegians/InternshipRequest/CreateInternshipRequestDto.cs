namespace Project.DTOs.Collegians
{
    public class CreateInternshipRequestDto
    {
        public string TermId { get; set; }
        public string FieldId { get; set; }

        public string InternshipLocationName { get; set; }
        public string InternshipLocationAddress { get; set; }
        public string InternshipLocationTel { get; set; }
        public string InternshipLocationSubject { get; set; }
        public string Days { get; set; }
        public string InternshipSupervisorName { get; set; }
    }
}
