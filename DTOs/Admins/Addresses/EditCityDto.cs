using FluentValidation;

namespace Project.DTOs.Admins.Addresses
{
    public class EditCityDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class EditCityDtoValidator : AbstractValidator<EditCityDto>
    {
        public EditCityDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();  
            RuleFor(x => x.Id).NotEmpty();            
        }
    }
}
