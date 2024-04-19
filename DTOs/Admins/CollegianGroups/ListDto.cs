using FluentValidation;
using Project.Core.AppPagedList;

namespace Project.DTOs.Admins.CollegianGroups
{
    public class ListDto : PagedListDto
    {
        public string InternshipLocationId { get; set; }
        public string MasterId { get; set; }
        public string FieldId { get; set; }
        public string TermId { get; set; }
        public string CollegianId { get; set; }
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
