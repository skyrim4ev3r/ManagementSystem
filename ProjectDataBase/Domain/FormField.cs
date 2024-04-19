using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.ProjectDataBase.Domain
{
    public class FormField
    {
        public string Id { get; set; }
        public string FormId { get; set; }
        public Form Form { get; set; }
        public string Value { get; set; }
    }
}
