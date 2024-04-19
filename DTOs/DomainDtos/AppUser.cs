using System;
using System.Collections.Generic;

namespace Project.DTOs.DomainDTOs
{
    public class AppUserD
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public CollegianD Collegian { get; set; }
        public MasterD Master { get; set; }
        public InternshipLocationD InternshipLocation { get; set; }
        public virtual ICollection<UserImageD> UserImages { get; set; }        
    }
}
