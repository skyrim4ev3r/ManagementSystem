using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Core;
using Project.Core.AppPagedList;
using Project.ProjectDataBase;
using Project.ProjectDataBase.Domain;
using Services.UserAccessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers.Adminss
{
    [ControllerName("Admins: Attendance")]
    [Route("Admins/[controller]")]
    [Authorize(Roles = Roles.Admin)]
    public class AttendanceController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ILogger<AttendanceController> _logger;
        private readonly IUserAccessor _userAccessor;

        public AttendanceController(DataContext context, ILogger<AttendanceController> logger,
            IUserAccessor userAccessor)
        {
            _context = context;
            _logger = logger;
            _userAccessor = userAccessor;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> List(ListDto dto)
        {
            try
            {
                var query = _context.Attendances
                    .Where(x => x.CollegianId == dto.CollegianId
                    && x.GroupId == dto.GroupId)
                    .OrderByDescending(x => x.CreatedAt);
                var res = await query.ToListAsync();                
                return Ok(Result<List<Attendance>>.Success(200, "عملیات با موفقیت انجام شد", res));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<string>.Failure(-1400, "عملیات با شکست مواجه شد"));
            }
        }
    }
    public class ListDto
    {
        public string CollegianId { get; set; }
        public string GroupId { get; set; }
    }
}  
  
