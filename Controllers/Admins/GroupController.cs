using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Core;
using Project.Core.AppPagedList;
using Project.DTOs.Admins.Groups;
using Project.DTOs.DomainDTOs;
using Project.ProjectDataBase;
using Project.ProjectDataBase.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers.Admins
{
    [ControllerName("Admins: Group")]
    [Route("Admins/[controller]")]
    [Authorize(Roles = Roles.Admin)]
    public class GroupController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;

        public GroupController(DataContext context, ILogger<UserController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ListCG(ListDto listDto)
        {
            try
            {
                var query = _context.Groups
                    .Include(x => x.Master)
                    .Include(x => x.Term)
                    .Include(x => x.Field)
                    .Include(x => x.InternshipLocation)                    
                    .OrderByDescending(x => x.Term.FinishAt)
                    .Where(x => x != null);
                if (!string.IsNullOrEmpty(listDto.FieldId))
                {
                    query = query.Where(x => x.FieldId == listDto.FieldId);
                }
                if (!string.IsNullOrEmpty(listDto.MasterId))
                {
                    query = query.Where(x => x.MasterId == listDto.MasterId);
                }
                if (!string.IsNullOrEmpty(listDto.TermId))
                {
                    query = query.Where(x => x.TermId == listDto.TermId);
                }
                if (!string.IsNullOrEmpty(listDto.InternshipLocationId))
                {
                    query = query.Where(x => x.InternshipLocationId == listDto.InternshipLocationId);
                }

                if (!string.IsNullOrEmpty(listDto.Filter))
                {
                    var sps = listDto.Filter.Split(' ');
                    foreach (var sp in sps)
                    {
                        query = query.Where(x =>
                        x.Field.Title.Contains(sp)
                        || x.Term.Title.Contains(sp)
                        || x.Master.FirstName.Contains(sp)
                        || x.Master.LastName.Contains(sp)
                        || x.InternshipLocation.Name.Contains(sp)
                        || x.InternshipLocation.Address.Contains(sp)
                        || x.InternshipLocation.Type.Contains(sp)
                        || x.InternshipLocation.Specialty.Contains(sp)
                        );
                    }
                }
                var res = await query.Select(x => new ListForCGDto 
                { 
                    Id = x.Id,
                    FullName = x.Master.FirstName+" "+x.Master.LastName+" "+
                    x.Field.Title+" "+x.Term.Title+" "+x.InternshipLocation.Name
                }).CreatePagedListAsync(listDto);
                return Ok(Result<PagedList<ListForCGDto>>.Success(200, "عملیات با موفقیت انجام شد", res));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<string>.Failure(-1400, "عملیات با شکست مواجه شد"));
            }
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> List(ListDto listDto)
        {
            try
            {
                var query = _context.Groups
                    .Include(x => x.Master)
                    .Include(x => x.Term)
                    .Include(x => x.Field)
                    .Include(x => x.InternshipLocation)
                    .Include(x => x.University)
                    .OrderByDescending(x => x.Term.FinishAt)
                    .Where(x => x != null);
                if (!string.IsNullOrEmpty(listDto.FieldId))
                {
                    query = query.Where(x => x.FieldId == listDto.FieldId);
                }
                if (!string.IsNullOrEmpty(listDto.MasterId))
                {
                    query = query.Where(x => x.MasterId == listDto.MasterId);
                }
                if (!string.IsNullOrEmpty(listDto.TermId))
                {
                    query = query.Where(x => x.TermId == listDto.TermId);
                }
                if (!string.IsNullOrEmpty(listDto.InternshipLocationId))
                {
                    query = query.Where(x => x.InternshipLocationId == listDto.InternshipLocationId);
                }

                if (!string.IsNullOrEmpty(listDto.Filter))
                {
                    var sps = listDto.Filter.Split(' ');
                    foreach (var sp in sps)
                    {
                        query = query.Where(x =>
                        x.Field.Title.Contains(sp)
                        || x.Term.Title.Contains(sp)
                        || x.TermName.Contains(sp)
                        || x.Master.FirstName.Contains(sp)
                        || x.Master.LastName.Contains(sp)
                        || x.InternshipLocation.Name.Contains(sp)
                        || x.InternshipLocation.Address.Contains(sp)
                        || x.InternshipLocation.Type.Contains(sp)
                        || x.InternshipLocation.Specialty.Contains(sp)
                        );
                    }
                }
                var res = await query.Select(x => _mapper.Map<GroupD>(x)).CreatePagedListAsync(listDto);
                return Ok(Result<PagedList<GroupD>>.Success(200, "عملیات با موفقیت انجام شد", res));
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
                _context.Groups.Add(new Group
                {
                    Id = Guid.NewGuid().ToString(),
                    TermId = string.IsNullOrEmpty(dto.TermId) ? null : dto.TermId,
                    InternshipLocationId = string.IsNullOrEmpty(dto.InternshipLocationId) ? null : dto.InternshipLocationId,
                    MasterId = string.IsNullOrEmpty(dto.MasterId) ? null : dto.MasterId,
                    FieldId = string.IsNullOrEmpty(dto.FieldId) ? null : dto.FieldId,
                    TermName = dto.TermName,
                    UniversityId = string.IsNullOrEmpty(dto.UniversityId) ? null : dto.UniversityId,
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
        public async Task<IActionResult> Edit(EditDto dto)
        {
            try
            {
                var gr = await _context.Groups.SingleOrDefaultAsync(x => x.Id == dto.Id);
                if (gr == null)
                    return BadRequest(Result<string>.Failure(-404, "ایتم پیدا نشد"));
                gr.TermId = string.IsNullOrEmpty(dto.TermId) ? null : dto.TermId;
                gr.InternshipLocationId = string.IsNullOrEmpty(dto.InternshipLocationId) ? null : dto.InternshipLocationId;
                gr.MasterId = string.IsNullOrEmpty(dto.MasterId) ? null : dto.MasterId;
                gr.FieldId = string.IsNullOrEmpty(dto.FieldId) ? null : dto.FieldId;
                gr.UniversityId = string.IsNullOrEmpty(dto.UniversityId) ? null : dto.UniversityId;
                gr.TermName = dto.TermName;
                _context.Groups.Update(gr);
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
                var gr = await _context.Groups.SingleOrDefaultAsync(x => x.Id == id);
                if (gr == null)
                    return BadRequest(Result<string>.Failure(-404, "ایتم پیدا نشد"));
                var c = await _context.CollegianGroups.CountAsync(x => x.GroupId == id);
                if (c > 0)
                    return BadRequest(Result<string>.Failure(-400, $"کاراموز در این گروه کارموزی هستند، برای حذف این گروه کاراموزی ابتدا دانشجویان مربوطه را از این گروه حذف کنید {c}"));
                _context.Groups.Remove(gr);
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
    public class ListForCGDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
    }
}

