using FluentValidation;

namespace Project.DTOs.Admins.Users
{
    public class ChangeUserPasswordDto
    {
        public string UserId { get; set; }
        public string NewPassword { get; set; }
    }
    public class ChangeUserPasswordDtoValidator : AbstractValidator<ChangeUserPasswordDto>
    {
        public ChangeUserPasswordDtoValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("ایدی نمیتواند خالی باشد");
            RuleFor(x => x.NewPassword).MinimumLength(8).WithMessage("طول رمز عبور نمیتواند کمتر از 8 کاراکتر باشد");
        }
    }
}
