using FluentValidation;

namespace Project.DTOs.Admins.Addresses
{
    public class ProvinceDto
    {
        public string Name { get; set; }
    }
    public class ProvinceDtoValidator : AbstractValidator<ProvinceDto>
    {
        public ProvinceDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
