using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.ProjectDataBase.Domain
{
    public class CollegianGroupFormField
    {
        public string Id { get; set; }
        public string CollegianId { get; set; }
        public string GroupId { get; set; }        
        public CollegianGroup CollegianGroup { get; set; }
        public string Value { get; set; }
    }
}
