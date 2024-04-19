using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.ProjectDataBase.Domain
{
    public class Form
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<FormField> FormFields { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
}
