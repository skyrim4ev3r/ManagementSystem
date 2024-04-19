using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Core;
using Project.Core.AppPagedList;
using Project.DTOs.Collegians;
using Project.DTOs.Masters.MasterEvaluations;
using Project.ProjectDataBase;
using Project.ProjectDataBase.Domain;
using Services.UserAccessors;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers.Admins
{
    [ControllerName("Admins: MasterEvaluation")]
    [Route("Admins/[controller]")]
    [Authorize(Roles = Roles.Admin)]
    public class MasterEvaluationController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ILogger<MasterEvaluationController> _logger;
        private readonly IUserAccessor _userAccessor;

        public MasterEvaluationController(DataContext context, ILogger<MasterEvaluationController> logger,
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
                var query = _context.MasterEvaluations
                    .Where(x => x.CollegianId == dto.CollegianId
                    && x.GroupId == dto.GroupId)
                    .OrderByDescending(x => x.CreatedAt);
                var res = await query.CreatePagedListAsync(dto);
                return Ok(Result<PagedList<MasterEvaluation>>.Success(200, "عملیات با موفقیت انجام شد", res));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<string>.Failure(-1400, "عملیات با شکست مواجه شد"));
            }
        }
    }
}

