using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ProjectDataBase.Domain
{
    public class City 
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string ProvinceId { get; set; }
        public Province Province { get; set; }
    }
}
