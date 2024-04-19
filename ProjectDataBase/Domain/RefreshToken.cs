using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ProjectDataBase.Domain
{
    public class RefreshToken
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
