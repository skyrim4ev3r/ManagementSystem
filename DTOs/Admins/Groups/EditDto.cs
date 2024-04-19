using FluentValidation;

namespace Project.DTOs.Admins.Groups
{
    public class EditDto
    {
        public string Id { get; set; }
        public string InternshipLocationId { get; set; }
        public string MasterId { get; set; }
        public string UniversityId { get; set; }
        public string FieldId { get; set; }
        public string TermId { get; set; }
        public string TermName { get; set; }
    }

    public class EditDtoValiadator : AbstractValidator<EditDto>
    {
        public EditDtoValiadator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
