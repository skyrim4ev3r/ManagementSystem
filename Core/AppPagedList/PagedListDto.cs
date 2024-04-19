using FluentValidation;

namespace Project.Core.AppPagedList
{
    public class PagedListDto
    {
        public int PageNumber { get; set; }
        public int ItemsCountPerPage { get; set; }
        public string Filter { get; set; }
    }

    public class PagedListDtoValidator : AbstractValidator<PagedListDto>
    {
        public PagedListDtoValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0).WithMessage("شماره صفحه باید بزرگتر از 0 باشد");
            RuleFor(x => x.ItemsCountPerPage)
                .GreaterThanOrEqualTo(1).WithMessage("تعداد ایتم ها در صفحه باید بیشتر یا مساوی با 1 باشد")
                .LessThanOrEqualTo(300).WithMessage("تعداد ایتم ها در صفحه باید کمتر یا مساوی با 300 باشد");
        }
    }
}
