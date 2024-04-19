using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.DTOs.Admins.Users
{
    public class CreateInternshipLocationDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Specialty { get; set; }
        public string Address { get; set; }
        public string CEOFullName { get; set; }
        public string CEOPhoneNumber { get; set; }
        public string TelNumber { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class CreateInternshipLocationDtoValidator : AbstractValidator<CreateInternshipLocationDto>
    {
        public CreateInternshipLocationDtoValidator()
        {
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("طول رمز عبور نمیتواند کمتر از 8 کاراکتر باشد");
            RuleFor(x => x.UserName).MinimumLength(8).WithMessage("طول نام کاربری نمیتواند کمتر از 8 کاراکتر باشد");
            RuleFor(x => x.Name).NotEmpty().WithMessage("نام نمیتواند خالی باشد");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("شماره تلفن نمیتواند خالی باشد");
            //RuleFor(x => x.Type).NotEmpty().WithMessage("تایپ ملی نمیتواند خالی باشد");
            //RuleFor(x => x.Specialty).NotEmpty().WithMessage("تخصص ملی نمیتواند خالی باشد");
            RuleFor(x => x.Address).NotEmpty().WithMessage("ادرس نمیتواند خالی باشد");
        }
    }
}
