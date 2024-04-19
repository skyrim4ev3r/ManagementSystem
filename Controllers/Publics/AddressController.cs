using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Core;
using Project.ProjectDataBase;
using Project.ProjectDataBase.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers.Publics
{
    [ControllerName("Public: Address")]
    [Route("Public/[controller]/[action]")]
    public class AddressController : BaseApiController
    {
        private readonly DataContext context;
        private readonly ILogger<AddressController> _logger;

        public AddressController(DataContext _context, ILogger<AddressController> logger)
        {
            context = _context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Provinces()
        {
            try
            {
                var result = await context.Provinces.ToListAsync();
                return Ok(Result<List<Province>>.Success(200, "عملیات با موفقیت انجام شد", result));
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<List<Province>>.Failure(-1400, "عملیات با شکست مواجه شد"));
            }
        }

        [HttpGet("{provinceId}")]
        public async Task<IActionResult> Cities(string provinceId)
        {
            try
            {
                var result = await context.Cities.Where(x => x.ProvinceId.Equals(provinceId)).ToListAsync();
                return Ok(Result<List<City>>.Success(200, "عملیات با موفقیت انجام شد", result));
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<List<City>>.Failure(-1400, "عملیات با شکست مواجه شد"));
            }
        }
    }
}

