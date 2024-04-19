using FluentValidation;
using Project.Core.AppPagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.DTOs.InternshipLocations.SupervisorEvaluationScores
{
    public class ListDto : PagedListDto
    {
        public string SupervisorEvaluationId { get; set; }
    }
    public class ListDtoValidator : AbstractValidator<ListDto>
    {
        public ListDtoValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0).WithMessage("شماره صفحه باید بزرگتر از 0 باشد");
            RuleFor(x => x.ItemsCountPerPage)
                .GreaterThanOrEqualTo(1).WithMessage("تعداد ایتم ها در صفحه باید بیشتر یا مساوی با 1 باشد")
                .LessThanOrEqualTo(300).WithMessage("تعداد ایتم ها در صفحه باید کمتر یا مساوی با 300 باشد");
        }
    }
}
