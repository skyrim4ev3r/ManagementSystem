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
using System.Linq;
using System.Threading.Tasks;
using Project.DTOs.Collegians.CollegianGroups;
using AutoMapper;
using Project.DTOs.DomainDTOs;

namespace Project.Controllers.Collegians
{
    [ControllerName("Collegians: CollegianGroup")]
    [Route("Collegians/[controller]")]
    [Authorize(Roles = Roles.Collegian)]
    public class CollegianGroupController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ILogger<CollegianGroupController> _logger;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;

        public CollegianGroupController(DataContext context, ILogger<CollegianGroupController> logger,
            IUserAccessor userAccessor, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _userAccessor = userAccessor;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> List(PagedListDto dto)
        {
            try
            {
                var query = _context.CollegianGroups
                    .OrderByDescending(x => x.CreatedAt)
                    .Include(x => x.Group)
                    .ThenInclude(x => x.Field)
                    .Include(x => x.Group)
                    .ThenInclude(x => x.University)
                    .Include(x => x.Group)
                    .ThenInclude(x => x.Term)
                    .Include(x => x.Group)
                    .ThenInclude(x => x.InternshipLocation)
                    .Include(x => x.Group)
                    .ThenInclude(x => x.Master)
                    .Where(x => x.CollegianId == _userAccessor.GetUserId());
                if (!string.IsNullOrEmpty(dto.Filter))
                {
                    var sps = dto.Filter.Split(' ');
                    foreach (var sp in sps)
                    {
                        query = query.Where(x =>
                        x.Collegian.FirstName.Contains(sp)
                        || x.Group.Master.LastName.Contains(sp)
                        || x.Group.Master.FatherName.Contains(sp)
                        || x.Group.Term.Title.Contains(sp)
                        || x.Group.Master.NationalNumber.Contains(sp)
                        || x.Group.Master.PhoneNumber.Contains(sp)
                        || x.Group.Field.Title.Contains(sp)
                        || x.Group.InternshipLocation.Name.Contains(sp)
                        );
                    }
                }
                var res = await query.Select(x => _mapper.Map<CollegianGroupD>(x)).CreatePagedListAsync(dto);
                return Ok(Result<PagedList<CollegianGroupD>>.Success(200, "عملیات با موفقیت انجام شد", res));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<string>.Failure(-1400, "عملیات با شکست مواجه شد"));
            }
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> EditCollegianScore(EditDto dto)
        {
            try
            {
                var cg = await _context.CollegianGroups
                    .FirstOrDefaultAsync(x => x.GroupId == dto.GroupId
                    && x.CollegianId == _userAccessor.GetUserId()
                    );
                if (cg == null)
                    return BadRequest(Result<string>.Failure(-404, "ایتم پیدا نشد"));
                cg.InternshipLocationScoreByCollegian = dto.Score;
                _context.CollegianGroups.Update(cg);
                await _context.SaveChangesAsync();
                return Ok(Result<PagedList<CollegianGroup>>.Success(200, "عملیات با موفقیت انجام شد"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<string>.Failure(-1400, "عملیات با شکست مواجه شد"));
            }
        }
    }
}

