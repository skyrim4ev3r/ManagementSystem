using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Core;
using Project.Core.AppPagedList;
using Project.DTOs.Admins.Users;
using Project.DTOs.Users.Users;
using Project.ProjectDataBase;
using Project.ProjectDataBase.Domain;
using Services.UserAccessors;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers.Users
{
    [ControllerName("Users: Field")]
    [Route("Users/[controller]/[action]")]
    //[Authorize]
    public class FieldController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ILogger<UserController> _logger;

        public FieldController(DataContext context, ILogger<UserController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> List(PagedListDto dto)
        {
            try
            {
                var query = _context.Fields.OrderByDescending(x => x.Title)
                    .Where(x => x.Title != null);
                if(dto.Filter != null)
                {
                    var sps = dto.Filter.Split(' ');
                    foreach(var sp in sps)
                    {
                        query = query.Where(x => x.Title.Contains(sp));
                    }
                }
                var res = await query.CreatePagedListAsync(dto);
                return Ok(Result<PagedList<Field>>.Success(200, "عملیات با موفقیت انجام شد", res));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<string>.Failure(-1400, "عملیات با شکست مواجه شد"));
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Listtt(PagedListDto dto)
        {
            try
            {
                var query = _context.Forms.OrderByDescending(x => x.Title)
                    .Where(x => x.Title != null);
                if(dto.Filter != null)
                {
                    var sps = dto.Filter.Split(' ');
                    foreach(var sp in sps)
                    {
                        query = query.Where(x => x.Title.Contains(sp));
                    }
                }
                var res = await query.CreatePagedListAsync(dto);
                return Ok(Result<PagedList<Form>>.Success(200, "عملیات با موفقیت انجام شد", res));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<string>.Failure(-1400, "عملیات با شکست مواجه شد"));
            }
        }
    }
}

