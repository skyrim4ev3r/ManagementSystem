using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Core;
using Project.Core.AppPagedList;
using Project.DTOs.Collegians;
using Project.ProjectDataBase;
using Project.ProjectDataBase.Domain;
using Services.UserAccessors;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers.Collegians
{
    [ControllerName("Collegians: InternshipRequest")]
    [Route("Collegians/[controller]")]
    [Authorize(Roles = Roles.Collegian)]
    public class InternshipRequestController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ILogger<InternshipRequestController> _logger;
        private readonly IUserAccessor _userAccessor;

        public InternshipRequestController(DataContext context, ILogger<InternshipRequestController> logger,
            IUserAccessor userAccessor)
        {
            _context = context;
            _logger = logger;
            _userAccessor = userAccessor;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> List(PagedListDto dto)
        {
            try
            {
                var query = _context.InternshipRequests
                    .Include(x => x.Field)
                    .Include(x => x.Term)
                    .OrderByDescending(x => x.CreatedAt)
                    .Where(x => x.CollegianId == _userAccessor.GetUserId());
                if(dto.Filter != null)
                {
                    var sps = dto.Filter.Split(' ');
                    foreach(var sp in sps)
                    {
                        query = query.Where(x => x.Field.Title.Contains(sp)
                        || x.Term.Title.Contains(sp)
                        || x.Collegian.FirstName.Contains(sp)
                        || x.Collegian.FatherName.Contains(sp)
                        || x.Collegian.LastName.Contains(sp)
                        || x.Collegian.PhoneNumber.Contains(sp)
                        || x.Collegian.TelNumber.Contains(sp)
                        || x.Collegian.User.UserName.Contains(sp));
                    }
                }
                var res = await query.CreatePagedListAsync(dto);
                return Ok(Result<PagedList<InternshipRequest>>.Success(200, "",res));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<string>.Failure(-1400, ""));
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(CreateInternshipRequestDto dto)
        {
            try
            {
                var interReq = new InternshipRequest
                {
                    Id = Guid.NewGuid().ToString(),
                    FieldId = string.IsNullOrEmpty(dto.FieldId) ? null : dto.FieldId,
                    CollegianId = _userAccessor.GetUserId(),
                    CreatedAt = DateTime.Now,
                    Days = dto.Days,
                    InternshipLocationAddress = dto.InternshipLocationAddress,
                    InternshipLocationName = dto.InternshipLocationName,
                    InternshipLocationTel = dto.InternshipLocationTel,
                    InternshipLocationSubject = dto.InternshipLocationSubject,
                    InternshipSupervisorName = dto.InternshipSupervisorName,
                    TermId = string.IsNullOrEmpty(dto.TermId) ? null : dto.TermId
                };
                _context.InternshipRequests.Add(interReq);
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

