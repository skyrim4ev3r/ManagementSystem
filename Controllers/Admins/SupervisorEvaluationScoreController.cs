using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Core;
using Project.Core.AppPagedList;
using Project.DTOs.InternshipLocations.SupervisorEvaluationScores;
using Project.ProjectDataBase;
using Project.ProjectDataBase.Domain;
using Services.UserAccessors;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers.Admins
{
    [ControllerName("Admins: SupervisorEvaluationScore")]
    [Route("Admins/[controller]")]
    [Authorize(Roles = Roles.Admin)]
    public class SupervisorEvaluationScoreController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ILogger<SupervisorEvaluationScoreController> _logger;
        private readonly IUserAccessor _userAccessor;

        public SupervisorEvaluationScoreController(DataContext context, ILogger<SupervisorEvaluationScoreController> logger,
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
                var query = _context.SupervisorEvaluationScores
                    .Where(x => x.SupervisorEvaluationId == dto.SupervisorEvaluationId)
                    .OrderByDescending(x => x.CreatedAt);
                var res = await query.CreatePagedListAsync(dto);
                return Ok(Result<PagedList<SupervisorEvaluationScore>>.Success(200, "عملیات با موفقیت انجام شد", res));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<string>.Failure(-1400, "عملیات با شکست مواجه شد"));
            }
        }
    }
}

