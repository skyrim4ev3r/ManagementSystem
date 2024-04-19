using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.Core;
using Project.Core.AppPagedList;
using Project.ProjectDataBase;
using Project.ProjectDataBase.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers.Users
{
    [ControllerName("Users: University")]
    [Route("Users/[controller]/[action]")]
    [Authorize]
    public class UniversityController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ILogger<UserController> _logger;

        public UniversityController(DataContext context, ILogger<UserController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> List(PagedListDto dto)
        {
            try
            {
                var query = _context.Universities.OrderBy(x => x.Title).AsQueryable();
                if (!string.IsNullOrEmpty(dto.Filter))
                {
                    var sps = dto.Filter.Split(' ');
                    foreach (var sp in sps)
                    {
                        query = query.Where(x => x.Title.Contains(sp));
                    }
                }
                var res = await query.CreatePagedListAsync(dto);
                return Ok(Result<PagedList<University>>.Success(200, "عملیات با موفقیت انجام شد", res));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<string>.Failure(-1400, "عملیات با شکست مواجه شد"));
            }
        }
    }
}

