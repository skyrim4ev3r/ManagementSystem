using FluentValidation;

namespace Project.DTOs.Admins.Addresses
{
    public class CityDto
    {
        public string ProvinceId { get; set; }
        public string Name { get; set; }
    }

    public class CityDtoValidator : AbstractValidator<CityDto>
    {
        public CityDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();  
            RuleFor(x => x.ProvinceId).NotEmpty().WithMessage("استان نمیتواند خالی باشد");
        }
    }
}
