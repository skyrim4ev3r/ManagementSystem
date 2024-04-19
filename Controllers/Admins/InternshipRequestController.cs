using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Core;
using Project.Core.AppPagedList;
using Project.ProjectDataBase;
using Project.ProjectDataBase.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers.Admins
{
    [ControllerName("Admins: InternshipRequest")]
    [Route("Admins/[controller]")]
    [Authorize(Roles = Roles.Admin)]
    public class InternshipRequestController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ILogger<InternshipRequestController> _logger;

        public InternshipRequestController(DataContext context, ILogger<InternshipRequestController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> List(PagedListDto dto)
        {
            try
            {
                var query = _context.InternshipRequests
                    .Include(x => x.Field)
                    .Include(x => x.Term)
                    .Include(x => x.Collegian)
                    .OrderByDescending(x => x.CreatedAt)
                    .Where(x => x.CollegianId != null);
                if (dto.Filter != null)
                {
                    var sps = dto.Filter.Split(' ');
                    foreach (var sp in sps)
                    {
                        query = query.Where(x => x.Field.Title.Contains(sp)
                        || x.Term.Title.Contains(sp));
                    }
                }
                var res = await query.CreatePagedListAsync(dto);
                return Ok(Result<PagedList<InternshipRequest>>.Success(200, "عملیات با موفقیت انجام شد", res));
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
                var interReq = await _context.InternshipRequests.FirstOrDefaultAsync(x => x.Id == id);
                _context.InternshipRequests.Remove(interReq);
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

