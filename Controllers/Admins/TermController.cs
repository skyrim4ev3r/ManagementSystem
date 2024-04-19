using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Core;
using Project.DTOs.Admins.Terms;
using Project.ProjectDataBase;
using Project.ProjectDataBase.Domain;
using System;
using System.Threading.Tasks;

namespace Project.Controllers.Admins
{
    [ControllerName("Admins: Term")]
    [Route("Admins/[controller]/[action]")]
    [Authorize(Roles = Roles.Admin)]
    public class TermController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ILogger<UserController> _logger;

        public TermController(DataContext context, ILogger<UserController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTermDto dto)
        {
            try
            {
                _context.Terms.Add(new Term
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = dto.Title,
                    FinishAt = dto.FinishAt,
                    StartAt = dto.StartAt,
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
        public async Task<IActionResult> Edit(EditTermDto dto)
        {
            try
            {
                var term = await _context.Terms.SingleOrDefaultAsync(x => x.Id == dto.Id);
                if (term == null)
                    return BadRequest(Result<string>.Failure(-404, "ایتم پیدا نشد"));
                term.Title = dto.Title;
                term.FinishAt = dto.FinishAt;
                term.StartAt = dto.StartAt;
                _context.Terms.Update(term);
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
                var term = await _context.Terms.SingleOrDefaultAsync(x => x.Id == id);
                if (term == null)
                    return BadRequest(Result<string>.Failure(-404, "ایتم پیدا نشد"));
                _context.Terms.Remove(term);
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

