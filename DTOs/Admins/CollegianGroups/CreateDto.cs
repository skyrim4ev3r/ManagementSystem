using FluentValidation;
using System;

namespace Project.DTOs.Admins.CollegianGroups
{
    public class CreateDto
    {
        public string CollegianId { get; set; }

        public string GroupId { get; set; }

        public DateTime? StartAt { get; set; }
        public DateTime? FinishAt { get; set; }
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
