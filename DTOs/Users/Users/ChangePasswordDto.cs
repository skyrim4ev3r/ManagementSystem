using FluentValidation;

namespace Project.DTOs.Users.Users
{
    public class ChangePasswordDto
    {
        public string OldPassword { get; set; }
        public string Password { get; set; }
    }
    public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidator()
        {
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("طول رمز عبور نمیتواند کمتر از 8 کاراکتر باشد");
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("طول رمز عبور قدیمی نمیتواند کمتر از 8 کاراکتر باشد");
        }
    }
}
