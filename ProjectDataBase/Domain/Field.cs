using System.Collections.Generic;

namespace Project.ProjectDataBase.Domain
{
    public class Field
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<InternshipRequest> InternshipRequests { get; set; }
    }
}
