using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Core;
using Project.Core.AppPagedList;
using Project.DTOs.DomainDTOs;
using Project.ProjectDataBase;
using Project.ProjectDataBase.Domain;
using Services.UserAccessors;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers.InternshipLocations
{
    [ControllerName("InternshipLocations: CollegianGroup")]
    [Route("InternshipLocations/[controller]")]
    [Authorize(Roles = Roles.InternshipLocation)]
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
                    .Include(x => x.Collegian)
                    .Include(x => x.Group)
                    .ThenInclude(x => x.Field)
                    .Include(x => x.Group)
                    .ThenInclude(x => x.Term)
                    .Include(x => x.Group)
                    .ThenInclude(x => x.University)
                    .Include(x => x.Group)
                    .ThenInclude(x => x.InternshipLocation)
                    .Include(x => x.Group)
                    .ThenInclude(x => x.Master)
                    .Include(x => x.Group)
                    .ThenInclude(x => x.Field)
                    .Where(x => x.Group.InternshipLocationId == _userAccessor.GetUserId());
                if (!string.IsNullOrEmpty(dto.Filter))
                {
                    var sps = dto.Filter.Split(' ');
                    foreach (var sp in sps)
                    {
                        query = query.Where(x =>
                        x.Collegian.FirstName.Contains(sp)
                        || x.Collegian.LastName.Contains(sp)
                        || x.Collegian.FatherName.Contains(sp)
                        || x.Group.Term.Title.Contains(sp)
                        || x.Collegian.NationalNumber.Contains(sp)
                        || x.Group.Field.Title.Contains(sp)
                        || x.Group.InternshipLocation.Name.Contains(sp)
                        || x.Collegian.PhoneNumber.Contains(sp)
                        || x.Collegian.TelNumber.Contains(sp));
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
    }
}

