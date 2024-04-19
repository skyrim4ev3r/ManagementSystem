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

namespace Project.Controllers.InternshipLocations
{
    [ControllerName("InternshipLocations: SupervisorEvaluationScore")]
    [Route("InternshipLocations/[controller]")]
    [Authorize(Roles = Roles.InternshipLocation)]
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
                    .Where(x => x.SupervisorEvaluationId == dto.SupervisorEvaluationId
                    && x.SupervisorEvaluation.CollegianGroup.Group.InternshipLocationId == _userAccessor.GetUserId())
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

        [HttpPost]
        public async Task<IActionResult> Create(CreateDto dto)
        {
            try
            {
                if(! await _context.SupervisorEvaluations.AnyAsync(x => x.Id == dto.SupervisorEvaluationId && x.CollegianGroup.Group.InternshipLocationId == _userAccessor.GetUserId()))
                    return BadRequest(Result<string>.Failure(-404, "ایتم پیدا نشد"));
                var se = new SupervisorEvaluationScore
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.Now,
                    SupervisorEvaluationId = dto.SupervisorEvaluationId,
                    Score = dto.Score,
                    Title = dto.Title,
                    Status = dto.Status,
                };
                _context.SupervisorEvaluationScores.Add(se);
                await _context.SaveChangesAsync();
                return Ok(Result<string>.Success(200, "عملیات با موفقیت انجام شد"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<string>.Failure(-1400, "عملیات با شکست مواجه شد"));
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> Edit(EditDto dto)
        {
            try
            {
                var se = await _context.SupervisorEvaluationScores.FirstOrDefaultAsync(x => x.Id == dto.Id && x.SupervisorEvaluation.CollegianGroup.Group.InternshipLocationId == _userAccessor.GetUserId());
                if (se == null)
                    return BadRequest(Result<string>.Failure(-404, "ایتم پیدا نشد"));
                se.Title = dto.Title;
                se.Score = dto.Score;
                se.Status = dto.Status;
                _context.SupervisorEvaluationScores.Update(se);
                await _context.SaveChangesAsync();
                return Ok(Result<string>.Success(200, "عملیات با موفقیت انجام شد"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<string>.Failure(-1400, "عملیات با شکست مواجه شد"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var se = await _context.SupervisorEvaluationScores.FirstOrDefaultAsync(x => x.Id == id && x.SupervisorEvaluation.CollegianGroup.Group.InternshipLocationId == _userAccessor.GetUserId());
                if (se == null)
                    return BadRequest(Result<string>.Failure(-404, "ایتم پیدا نشد"));
                _context.SupervisorEvaluationScores.Remove(se);
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

