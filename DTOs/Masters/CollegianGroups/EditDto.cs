using FluentValidation;
using Project.Core.AppPagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.DTOs.Masters.CollegianGroups
{
    public class EditDto
    {
        public string CollegianId { get; set; }
        public string GroupId { get; set; }
        public int Score { get; set; }
    }
}
