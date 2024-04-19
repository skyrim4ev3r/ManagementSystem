using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Core;
using Project.Core.AppPagedList;
using Project.DTOs.Admins.CollegianGroups;
using Project.DTOs.DomainDTOs;
using Project.ProjectDataBase;
using Project.ProjectDataBase.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers.Admins
{
    [ControllerName("Admins: CollegianGroup")]
    [Route("Admins/[controller]")]
    [Authorize(Roles = Roles.Admin)]
    public class CollegianGroupController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;

        public CollegianGroupController(DataContext context, ILogger<UserController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> List(PagedListDto listDto)
        {
            try
            {
                var query = _context.CollegianGroups
                    .Include(x => x.Group)
                    .ThenInclude(x => x.Master)
                    .Include(x => x.Group)
                    .ThenInclude(x => x.Term)
                    .Include(x => x.Group)
                    .ThenInclude(x => x.University)
                    .Include(x => x.Group)
                    .ThenInclude(x => x.Field)
                    .Include(x => x.Group)
                    .ThenInclude(x => x.InternshipLocation)
                    .Include(x => x.Collegian)
                    .OrderByDescending(x => x.CreatedAt)
                    .Where(x => x != null);

                if (!string.IsNullOrEmpty(listDto.Filter))
                {
                    var sps = listDto.Filter.Split(' ');
                    foreach (var sp in sps)
                    {
                        query = query.Where(x =>
                        x.Group.Field.Title.Contains(sp)
                        || x.Group.Term.Title.Contains(sp)
                        || x.Group.Master.FirstName.Contains(sp)
                        || x.Group.Master.LastName.Contains(sp)
                        || x.Group.InternshipLocation.Name.Contains(sp)
                        || x.Group.InternshipLocation.Address.Contains(sp)
                        || x.Group.InternshipLocation.Type.Contains(sp)
                        || x.Group.InternshipLocation.Specialty.Contains(sp)
                        || x.Collegian.FirstName.Contains(sp)
                        || x.Collegian.FatherName.Contains(sp)
                        || x.Collegian.LastName.Contains(sp)
                        || x.Collegian.PhoneNumber.Contains(sp)
                        || x.Collegian.TelNumber.Contains(sp)
                        || x.Collegian.User.UserName.Contains(sp)
                        || x.Group.InternshipLocation.User.UserName.Contains(sp)
                        || x.Group.Master.User.UserName.Contains(sp)
                        );
                    }
                }
                var res = await query.Select(x => _mapper.Map<CollegianGroupD>(x)).CreatePagedListAsync(listDto);
                return Ok(Result<PagedList<CollegianGroupD>>.Success(200, "عملیات با موفقیت انجام شد", res));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<string>.Failure(-1400, "عملیات با شکست مواجه شد"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDto dto)
        {
            try
            {
                _context.CollegianGroups.Add(new CollegianGroup
                {
                    CollegianId = dto.CollegianId,
                    GroupId = dto.GroupId,
                    FinishAt = dto.FinishAt,
                    StartAt = dto.StartAt,
                    InternshipLocationScoreByCollegian = 0,
                    InternshipLocationScoreByMaster = 0,
                });
                await _context.SaveChangesAsync();
                return Ok(Result<string>.Success(200, "عملیات با موفقیت انجام شد"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<string>.Failure(-1400, "عملیات با شکست مواجه شد"));
            }
        }
        
        [HttpDelete("{collegianId}/{groupId}")]
        public async Task<IActionResult> Delete(string collegianId, string groupId)
        {
            try
            {
                var cg = await _context.CollegianGroups.SingleOrDefaultAsync(x => x.CollegianId == collegianId && x.GroupId == groupId);
                if (cg == null)
                    return BadRequest(Result<string>.Failure(-404, "ایتم پیدا نشد"));
                _context.CollegianGroups.Remove(cg);
                await _context.SaveChangesAsync();
                return Ok(Result<string>.Success(200, "عملیات با موفقیت انجام شد"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<string>.Failure(-1400, "عملیات با شکست مواجه شد"));
            }
        }
    }
}

