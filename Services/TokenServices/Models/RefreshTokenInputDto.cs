using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Services.TokenServices.Models
{
    public class RefreshTokenInputDto
    {
        [Required]
        [MinLength(5)]
        public string RefreshToken { get; set; }
    }
}
