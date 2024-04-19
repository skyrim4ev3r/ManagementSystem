using System.Collections.Generic;

namespace Project.ProjectDataBase.Domain
{
    public class University
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}
