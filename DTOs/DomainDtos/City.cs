using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DTOs.DomainDTOs
{
    public class CityD 
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string ProvinceId { get; set; }
        public ProvinceD Province { get; set; }
    }
}
