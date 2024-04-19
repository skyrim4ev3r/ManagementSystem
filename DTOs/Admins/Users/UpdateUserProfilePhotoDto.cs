using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Project.DTOs.Admins.Users
{
    public class UpdateUserProfilePhotoDto
    {
        public IFormFile ProfilePhotoFile { get; set; }
    }
    //public class UpdateUserProfilePhotoDtoValidator : AbstractValidator<UpdateUserProfilePhotoDto>
    //{
    //    public UpdateUserProfilePhotoDtoValidator()
    //    {

    //    }
    //}
}
