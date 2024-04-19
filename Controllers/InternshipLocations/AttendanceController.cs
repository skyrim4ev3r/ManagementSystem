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
using Project.DTOs.InternshipLocations.Attendances;

namespace Project.Controllers.InternshipLocations
{
    [ControllerName("InternshipLocations: Attendance")]
    [Route("InternshipLocations/[controller]")]
    [Authorize(Roles = Roles.InternshipLocation)]
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
                    && x.GroupId == dto.GroupId
                    && x.CollegianGroup.Group.InternshipLocationId == _userAccessor.GetUserId())
                    .OrderByDescending(x => x.CreatedAt);
                //if(dto.Filter != null)
                //{
                //    var sps = dto.Filter.Split(' ');
                //    foreach(var sp in sps)
                //    {
                //        query = query.Where(x => x.Field.Title.Contains(sp)
                //        || x.Term.Title.Contains(sp));
                //    }
                //}
                var res = await query.ToListAsync();
                //var res = await query.CreatePagedListAsync(dto);
                //return Ok(Result<PagedList<MasterEvaluation>>.Success(200, "عملیات با موفقیت انجام شد", res));
                return Ok(Result<List<Attendance>>.Success(200, "عملیات با موفقیت انجام شد", res));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<string>.Failure(-1400, "عملیات با شکست مواجه شد"));
            }
        }

        [HttpPost("GetState")]
        public async Task<IActionResult> GetState(ListDto dto)
        {
            try
            {
                if (!await _context.CollegianGroups.AnyAsync(x => x.CollegianId == dto.CollegianId
                     && x.GroupId == dto.GroupId
                     && x.Group.InternshipLocationId == _userAccessor.GetUserId()))
                    return NotFound();
                var res = (await _context.Attendances
                    .Where(x => x.CollegianId == dto.CollegianId
                    && x.GroupId == dto.GroupId)
                    .OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync());
                if (res == null)
                    return Ok(Result<string>.Success(200, "عملیات با موفقیت انجام شد", "Exit"));
                var state = res.FinishAt == null ? "Enter" : "Exit";
                
                return Ok(Result<string>.Success(200, "عملیات با موفقیت انجام شد",state));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<string>.Failure(-1400, "عملیات با شکست مواجه شد"));
            }
        }

        [HttpPost("ChangeState")]
        public async Task<IActionResult> ChangeState(ChangeStateDto dto)
        {
            try
            {
                if (!await _context.CollegianGroups.AnyAsync(x => x.CollegianId == dto.CollegianId
                     && x.GroupId == dto.GroupId
                     && x.Group.InternshipLocationId == _userAccessor.GetUserId()))
                    return NotFound();
                var res = (await _context.Attendances
                    .Where(x => x.CollegianId == dto.CollegianId
                    && x.GroupId == dto.GroupId)
                    .OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync());
                if(res != null && res.FinishAt == null)
                {
                    res.FinishAt = DateTime.Now;
                    _context.Attendances.Update(res);
                    await _context.SaveChangesAsync();
                    return Ok(Result<string>.Success(200, "خروج با موفقیت ثبت شد"));
                }
                _context.Attendances.Add(new Attendance
                {
                    Id = Guid.NewGuid().ToString(),
                    CollegianId = dto.CollegianId,
                    GroupId = dto.GroupId,
                    CreatedAt = DateTime.Now,
                    StartedAt = DateTime.Now,
                    FinishAt = null,
                });
                await _context.SaveChangesAsync();
                return Ok(Result<string>.Success(200, "ورود با موفقیت انجام شد"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<string>.Failure(-1400, "عملیات با شکست مواجه شد"));
            }
        }
    }    
}

