using FluentValidation;

namespace Project.DTOs.Admins.Addresses
{
    public class EditProvinceDto
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }
    public class EditProvinceDtoValidator : AbstractValidator<EditProvinceDto>
    {
        public EditProvinceDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Id).NotEmpty();            
        }
    }
}
