using FluentValidation;

namespace Project.DTOs.Masters.MasterEvaluations
{
    public class CreateDto
    {
        public string CollegianId { get; set; }
        public string GroupId { get; set; }
        public string Title { get; set; }
    }
    public class CreateDtoValidator : AbstractValidator<CreateDto>
    {
        public CreateDtoValidator()
        {
            RuleFor(x => x.CollegianId).NotEmpty();
            RuleFor(x => x.GroupId).NotEmpty();
        }
    }
}
