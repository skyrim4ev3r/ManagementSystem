using System;
using System.Collections.Generic;

namespace Project.ProjectDataBase.Domain
{
    public class Term
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime FinishAt { get; set; }
        public ICollection<Group> Groups { get; set; }
        public virtual ICollection<InternshipRequest> InternshipRequests { get; set; }
    }
}
