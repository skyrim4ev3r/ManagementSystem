using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ProjectDataBase.Domain
{
    public class AppUser : IdentityUser
    {
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }       

        public Collegian Collegian { get; set; }
        public Master Master { get; set; }
        public InternshipLocation InternshipLocation { get; set; }

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }        
        public virtual ICollection<UserImage> UserImages { get; set; }        
    }
}
