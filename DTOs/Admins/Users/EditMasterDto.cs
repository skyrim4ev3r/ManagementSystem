using FluentValidation;

namespace Project.DTOs.Admins.Users
{
    public class EditMasterDto
    {
        public string PhoneNumber { get; set; }
        public string UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string NationalNumber { get; set; }
        public string DegreeCode { get; set; }
        public string CourseCode { get; set; }

    }

    public class EditMasterDtoValidator : AbstractValidator<EditMasterDto>
    {
        public EditMasterDtoValidator()
        {           
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("نام نمیتواند خالی باشد");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("ایدی نمیتواند خالی باشد");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("نام خانوادگی نمیتواند خالی باشد");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("شماره تلفن نمیتواند خالی باشد");
            RuleFor(x => x.NationalNumber).NotEmpty().WithMessage("کد ملی نمیتواند خالی باشد");
        }
    }

}
