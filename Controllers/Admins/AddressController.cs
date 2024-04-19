using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Core;
using Project.DTOs.Admins.Addresses;
using Project.ProjectDataBase;
using Project.ProjectDataBase.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers.Admins
{
    [ControllerName("Admins: Address")]
    [Route("Admins/[controller]/[action]")]
    [Authorize(Roles = Roles.Admin)]
    public class AddressController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ILogger<AddressController> _logger;

        public AddressController(DataContext context, ILogger<AddressController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProvince(ProvinceDto province)
        {
            try
            {
                var p = new Province
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = province.Name,
                };
                var result = _context.Provinces.Add(p);
                await _context.SaveChangesAsync();
                return Ok(Result<string>.Success(200,"استان اضافه شد"));
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<List<Province>>.Failure(-1400, ""));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity(CityDto city)
        {
            try
            {
                var c = new City
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = city.Name,
                    ProvinceId = city.ProvinceId
                };
                var result = _context.Cities.Add(c);
                await _context.SaveChangesAsync();
                return Ok(Result<string>.Success(200, "شهر اضافه شد"));
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<List<City>>.Failure(-1400, ""));
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditProvince(EditProvinceDto dto)
        {
            try
            {
                var p = _context.Provinces.FirstOrDefault(x => x.Id == dto.Id);
                p.Name = dto.Name;
                _context.Provinces.Update(p);
                await _context.SaveChangesAsync();
                return Ok(Result<string>.Success(200, "استان ویرایش شد"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<List<Province>>.Failure(-1400, ""));
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditCity(EditCityDto dto)
        {
            try
            {
                var p = _context.Cities.FirstOrDefault(x => x.Id == dto.Id);
                p.Name = dto.Name;
                _context.Cities.Update(p);
                await _context.SaveChangesAsync();
                return Ok(Result<string>.Success(200, "شهر ویرایش شد"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<List<City>>.Failure(-1400, ""));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvince(string id)
        {
            try
            {
                var p = await _context.Provinces.FirstOrDefaultAsync(x => x.Id == id);
                _context.Provinces.Remove(p);
                await _context.SaveChangesAsync();
                return Ok(Result<string>.Success(200, "استان حذف شد"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<List<Province>>.Failure(-1400, ""));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(string id)
        {
            try
            {
                var p = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);
                _context.Cities.Remove(p);
                await _context.SaveChangesAsync();
                return Ok(Result<string>.Success(200, "شهر حذف شد"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<List<City>>.Failure(-1400, ""));
            }
        }
    }
}

