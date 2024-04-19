using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.DTOs.Admins.Users
{
    public class EditCollegianDto
    {
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string CityId { get; set; }
        public string Address { get; set; }
        public string NationalNumber { get; set; }
        public string TelNumber { get; set; }
        public string UniversityCode { get; set; }
        public string EducationCode { get; set; }
        public char Degree { get; set; }
        public char Nobat { get; set; }
    }

    public class EditCollegianDtoValidator : AbstractValidator<EditCollegianDto>
    {
        public EditCollegianDtoValidator()
        {            
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("نام نمیتواند خالی باشد");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("ایدی نمیتواند خالی باشد");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("نام خانوادگی نمیتواند خالی باشد");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("شماره تلفن نمیتواند خالی باشد");
            RuleFor(x => x.NationalNumber).NotEmpty().WithMessage("کد ملی نمیتواند خالی باشد");
        }
    }
}
