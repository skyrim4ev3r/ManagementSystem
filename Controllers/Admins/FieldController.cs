using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Core;
using Project.Core.AppPagedList;
using Project.DTOs.Admins.Fields;
using Project.ProjectDataBase;
using Project.ProjectDataBase.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers.Admins
{
    [ControllerName("Admins: Field")]
    [Route("Admins/[controller]")]
    [Authorize(Roles = Roles.Admin)]
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
        public async Task<IActionResult> Create(CreateFieldDto dto)
        {
            try
            {
                _context.Fields.Add(new Field
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = dto.Title,
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
        
        [HttpPut]
        public async Task<IActionResult> Edit(EditFieldDto dto)
        {
            try
            {
                var field = await _context.Fields.SingleOrDefaultAsync(x => x.Id == dto.Id);
                if (field == null)
                    return BadRequest(Result<string>.Failure(-404, "ایتم پیدا نشد"));
                field.Title = dto.Title;
                _context.Fields.Update(field);
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
                var field = await _context.Fields.SingleOrDefaultAsync(x => x.Id == id);
                if (field == null)
                    return BadRequest(Result<string>.Failure(-404, "ایتم پیدا نشد"));
                
                _context.Fields.Remove(field);
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

