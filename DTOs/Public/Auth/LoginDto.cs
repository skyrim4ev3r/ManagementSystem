using FluentValidation;

namespace Project.DTOs.Auth
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CaptchaId { get; set; }
        public string CaptchaCode { get; set; }
    }

    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("نام کاربری نمیتواند خالی باشد");
            RuleFor(x => x.Password).NotEmpty().WithMessage("کلمه عبور نمیتواند خالی باشد");
        }
    }
}
