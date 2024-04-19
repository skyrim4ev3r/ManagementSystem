using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Core;
using Project.Core.AppPagedList;
using Project.DTOs.Masters.MasterEvaluationScores;
using Project.ProjectDataBase;
using Project.ProjectDataBase.Domain;
using Services.UserAccessors;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers.Masters
{
    [ControllerName("Masters: MasterEvaluationScore")]
    [Route("Masters/[controller]")]
    [Authorize(Roles = Roles.Master)]
    public class MasterEvaluationScoreController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ILogger<MasterEvaluationController> _logger;
        private readonly IUserAccessor _userAccessor;

        public MasterEvaluationScoreController(DataContext context, ILogger<MasterEvaluationController> logger,
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
                var query = _context.MasterEvaluationScores
                    .Where(x => x.MasterEvaluationId == dto.MasterEvaluationId
                    && x.MasterEvaluation.CollegianGroup.Group.MasterId == _userAccessor.GetUserId())
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
                var res = await query.CreatePagedListAsync(dto);
                return Ok(Result<PagedList<MasterEvaluationScore>>.Success(200, "عملیات با موفقیت انجام شد", res));
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
                if(! await _context.MasterEvaluations.AnyAsync(x => x.Id == dto.MasterEvaluationId && x.CollegianGroup.Group.MasterId == _userAccessor.GetUserId()))
                    return BadRequest(Result<string>.Failure(-404, "ایتم پیدا نشد"));
                var me = new MasterEvaluationScore
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.Now,
                    MasterEvaluationId = dto.MasterEvaluationId,
                    Score = dto.Score,
                    Title = dto.Title,
                    MaxScore = dto.MaxScore,
                };
                _context.MasterEvaluationScores.Add(me);
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
                var me = await _context.MasterEvaluationScores.FirstOrDefaultAsync(x => x.Id == dto.Id && x.MasterEvaluation.CollegianGroup.Group.MasterId == _userAccessor.GetUserId());

                if(me == null)
                    return BadRequest(Result<string>.Failure(-404, "ایتم پیدا نشد"));
                me.Title = dto.Title;
                me.MaxScore = dto.MaxScore;
                me.Score = dto.Score;
                _context.MasterEvaluationScores.Update(me);
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
                var me = await _context.MasterEvaluationScores.FirstOrDefaultAsync(x => x.Id == id && x.MasterEvaluation.CollegianGroup.Group.MasterId == _userAccessor.GetUserId());
                if (me == null)
                    return BadRequest(Result<string>.Failure(-404, "ایتم پیدا نشد"));
                _context.MasterEvaluationScores.Remove(me);
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

