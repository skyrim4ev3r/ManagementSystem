using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.DTOs.Admins.Users
{
    public class EditInternshipLocationDto
    {
        public string PhoneNumber { get; set; }
        public string UserId { get; set; }
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

    public class EditInternshipLocationDtoValidator : AbstractValidator<EditInternshipLocationDto>
    {
        public EditInternshipLocationDtoValidator()
        {           
            RuleFor(x => x.Name).NotEmpty().WithMessage("نام نمیتواند خالی باشد");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("ایدی نمیتواند خالی باشد");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("شماره تلفن نمیتواند خالی باشد");
            //RuleFor(x => x.Type).NotEmpty().WithMessage("تایپ ملی نمیتواند خالی باشد");
            //RuleFor(x => x.Specialty).NotEmpty().WithMessage("تخصص ملی نمیتواند خالی باشد");
            RuleFor(x => x.Address).NotEmpty().WithMessage("ادرس نمیتواند خالی باشد");
        }
    }
}
