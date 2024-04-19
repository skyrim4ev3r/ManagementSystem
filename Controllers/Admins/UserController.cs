using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Core;
using Project.Core.AppPagedList;
using Project.DTOs.Admins.Users;
using Project.DTOs.DomainDTOs;
using Project.Extensions;
using Project.ProjectDataBase;
using Project.ProjectDataBase.Domain;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers.Admins
{
    [ControllerName("Admins: User")]
    [Route("Admins/[controller]/[action]")]
    [Authorize(Roles = Roles.Admin)]
    public class UserController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public UserController(DataContext context, ILogger<UserController> logger,
            UserManager<AppUser> userManager, IWebHostEnvironment environment, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _environment = environment;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCollegian(CreateCollegianDto dto)
        {
            try
            {
                if (await _context.Users.AnyAsync(x => x.NormalizedUserName == dto.UserName.ToUpper()))
                    return BadRequest(Result<string>.Success(-400, "نام کاربری تکراری است"));
                string cityId = null;
                if (await _context.Cities.AnyAsync(x => x.Id == dto.CityId))
                    cityId = dto.CityId;
                var user = new AppUser
                {
                    CreatedAt = DateTime.Now,
                    IsDeleted = false,
                    PhoneNumber = dto.PhoneNumber,
                    UserName = dto.UserName
                };
                var identityResult = _userManager.CreateAsync(user, dto.Password).Result;
                if (!identityResult.Succeeded)
                {
                    throw new Exception();
                }
                _context.Collegians.Add(new Collegian
                {
                    Id = user.Id,
                    UserId = user.Id,
                    CreatedAt = DateTime.Now,
                    CityId = cityId,
                    NationalNumber = dto.NationalNumber,
                    PhoneNumber = dto.PhoneNumber,
                    LastName = dto.LastName,
                    FirstName = dto.FirstName,
                    Address = dto.Address,
                    EducationCode = dto.EducationCode,
                    FatherName = dto.FatherName,
                    UniversityCode = dto.UniversityCode,
                    TelNumber = dto.TelNumber,
                    Degree = dto.Degree,
                    Nobat = dto.Nobat,
                });
                await _userManager.AddToRoleAsync(user, Roles.Collegian);

                await _context.SaveChangesAsync();

                return Ok(Result<string>.Success(200, "دانشجو با موفقیت ثبت نام شد"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<string>.Failure(-1400, ""));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMaster(CreateMasterDto dto)
        {
            try
            {
                if (await _context.Users.AnyAsync(x => x.NormalizedUserName == dto.UserName.ToUpper()))
                    return BadRequest(Result<string>.Success(-400, "نام کاربری تکراری است"));
                var user = new AppUser
                {
                    CreatedAt = DateTime.Now,
                    IsDeleted = false,
                    PhoneNumber = dto.PhoneNumber,
                    UserName = dto.UserName
                };
                var identityResult = _userManager.CreateAsync(user, dto.Password).Result;
                if (!identityResult.Succeeded)
                {
                    throw new Exception();
                }
                _context.Masters.Add(new Master
                {
                    Id = user.Id,
                    UserId = user.Id,
                    FatherName = dto.FatherName,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    PhoneNumber = dto.PhoneNumber,
                    NationalNumber = dto.NationalNumber,
                    CourseCode = dto.CourseCode,
                    CreatedAt = DateTime.Now,
                    DegreeCode = dto.DegreeCode,
                });
                await _userManager.AddToRoleAsync(user, Roles.Master);

                await _context.SaveChangesAsync();

                return Ok(Result<string>.Success(200, "استاد با موفقیت ثبت نام شد"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<string>.Failure(-1400, ""));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateInternshipLocation(CreateInternshipLocationDto dto)
        {
            try
            {
                if (await _context.Users.AnyAsync(x => x.NormalizedUserName == dto.UserName.ToUpper()))
                    return BadRequest(Result<string>.Success(-400, "نام کاربری تکراری است"));
                var user = new AppUser
                {
                    CreatedAt = DateTime.Now,
                    IsDeleted = false,
                    PhoneNumber = dto.PhoneNumber,
                    UserName = dto.UserName
                };
                var identityResult = _userManager.CreateAsync(user, dto.Password).Result;
                if (!identityResult.Succeeded)
                {
                    throw new Exception();
                }
                _context.InternshipLocations.Add(new InternshipLocation
                {
                    Id = user.Id,
                    UserId = user.Id,
                    CreatedAt = DateTime.Now,
                    Address = dto.Address,
                    Name = dto.Name,
                    Specialty = dto.Specialty,
                    Type = dto.Type,
                    X = dto.X,
                    Y = dto.Y,
                    CEOFullName = dto.CEOFullName,
                    CEOPhoneNumber = dto.CEOPhoneNumber,
                    TelNumber = dto.TelNumber
                });
                await _userManager.AddToRoleAsync(user, Roles.InternshipLocation);

                await _context.SaveChangesAsync();

                return Ok(Result<string>.Success(200, "محل کاراموزی با موفقیت ثبت نام شد"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<string>.Failure(-1400, ""));
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditCollegian(EditCollegianDto dto)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == dto.UserId);
                var collegian = await _context.Collegians.FirstOrDefaultAsync(x => x.Id == dto.UserId);
                if (user == null || collegian == null)
                    return BadRequest(Result<string>.Success(-400, "دانشجو پیدا نشد"));

                if (await _context.Cities.AnyAsync(x => x.Id == dto.CityId))
                    collegian.CityId = dto.CityId;
                user.PhoneNumber = dto.PhoneNumber;
                collegian.PhoneNumber = dto.PhoneNumber;
                collegian.TelNumber = dto.TelNumber;
                collegian.Address = dto.Address;
                collegian.LastName = dto.LastName;
                collegian.FirstName = dto.FirstName;
                collegian.EducationCode = dto.EducationCode;
                collegian.FatherName = dto.FatherName;
                collegian.UniversityCode = dto.UniversityCode;
                collegian.NationalNumber = dto.NationalNumber;
                collegian.Nobat = dto.Nobat;
                collegian.Degree = dto.Degree;

                await _context.SaveChangesAsync();

                return Ok(Result<string>.Success(200, "دانشجو با موفقیت ویرایش شد"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<string>.Failure(-1400, ""));
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditMaster(EditMasterDto dto)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == dto.UserId);
                var master = await _context.Masters.FirstOrDefaultAsync(x => x.Id == dto.UserId);
                if (user == null || master == null)
                    return BadRequest(Result<string>.Success(-400, "استاد پیدا نشد"));
                user.PhoneNumber = dto.PhoneNumber;
                master.PhoneNumber = dto.PhoneNumber;
                master.LastName = dto.LastName;
                master.FirstName = dto.FirstName;
                master.FatherName = dto.FatherName;
                master.NationalNumber = dto.NationalNumber;
                master.DegreeCode = dto.DegreeCode;
                master.CourseCode = dto.CourseCode;
                _context.Masters.Update(master);
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return Ok(Result<string>.Success(200, "استاد با موفقیت ویرایش شد"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<string>.Failure(-1400, ""));
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditInternshipLocation(EditInternshipLocationDto dto)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == dto.UserId);
                var intern = await _context.InternshipLocations.FirstOrDefaultAsync(x => x.Id == dto.UserId);
                if (user == null || intern == null)
                    return BadRequest(Result<string>.Success(-400, "استاد پیدا نشد"));
                user.PhoneNumber = dto.PhoneNumber;
                intern.Address = dto.Address;
                intern.Name = dto.Name;
                intern.Specialty = dto.Specialty;
                intern.Type = dto.Type;
                intern.X = dto.X;
                intern.Y = dto.Y;
                intern.CEOFullName = dto.CEOFullName;
                intern.CEOPhoneNumber = dto.CEOPhoneNumber;
                intern.TelNumber = dto.TelNumber;
                _context.InternshipLocations.Update(intern);
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return Ok(Result<string>.Success(200, "محل کاراموزی با موفقیت ویرایش شد"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<string>.Failure(-1400, ""));
            }
        }

        [HttpPost]
        public async Task<IActionResult> ListCollegian(PagedListDto dto)
        {
            var query = _context.Users
                .Include(x => x.Collegian)
                .ThenInclude(x => x.City)
                .Include(x => x.UserImages)
                .Where(x => x.Collegian != null);
            if (!string.IsNullOrEmpty(dto.Filter))
            {
                var sps = dto.Filter.Split(' ');
                foreach (var sp in sps)
                    query = query
                        .Where(x => x.Collegian.FirstName.Contains(sp)
                        || x.Collegian.LastName.Contains(sp)
                        || x.Collegian.FatherName.Contains(sp)
                        || x.Collegian.NationalNumber.Contains(sp)
                        || x.Collegian.PhoneNumber.Contains(sp));
            }
            var users = await query.Select(x => _mapper.Map<AppUserD>(x)).CreatePagedListAsync(dto);
            return Ok(Result<PagedList<AppUserD>>.Success(200, "عملیات با موفقیت انجام شد", users));
        }

        [HttpPost]
        public async Task<IActionResult> ListMaster(PagedListDto dto)
        {
            var query = _context.Users
                .Include(x => x.Master)
                .Include(x => x.UserImages)
                .Where(x => x.Master != null);
            if (!string.IsNullOrEmpty(dto.Filter))
            {
                var sps = dto.Filter.Split(' ');
                foreach (var sp in sps)
                    query = query
                        .Where(x => x.Master.FirstName.Contains(sp)
                        || x.Master.LastName.Contains(sp)
                        || x.Master.FatherName.Contains(sp)
                        || x.Master.NationalNumber.Contains(sp)
                        || x.Master.PhoneNumber.Contains(sp));
            }
            var users = await query.Select(x => _mapper.Map<AppUserD>(x)).CreatePagedListAsync(dto);
            return Ok(Result<PagedList<AppUserD>>.Success(200, "عملیات با موفقیت انجام شد", users));
        }

        [HttpPost]
        public async Task<IActionResult> ListInternLocation(PagedListDto dto)
        {
            var query = _context.Users
                .Include(x => x.InternshipLocation)
                .Include(x => x.UserImages)
                .Where(x => x.InternshipLocation != null);
            if (!string.IsNullOrEmpty(dto.Filter))
            {
                var sps = dto.Filter.Split(' ');
                foreach (var sp in sps)
                    query = query
                        .Where(x => x.InternshipLocation.Address.Contains(sp)
                        || x.InternshipLocation.Name.Contains(sp)
                        || x.InternshipLocation.Specialty.Contains(sp)
                        || x.InternshipLocation.Type.Contains(sp));
            }
            var users = await query.Select(x => _mapper.Map<AppUserD>(x)).CreatePagedListAsync(dto);
            return Ok(Result<PagedList<AppUserD>>.Success(200, "عملیات با موفقیت انجام شد", users));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Confirm(string id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return HandleResult(Result<string>.Failure(-400, "کاربر پیدا نشد"));
            user.IsDeleted = !user.IsDeleted;
            if (user.IsDeleted)
                user.DeletedAt = DateTime.Now;
            else
            {
                user.DeletedAt = null;
            }
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return Ok(Result<PagedList<AppUser>>.Success(200, "عملیات با موفقیت انجام شد"));
        }

        [HttpPut]
        public async Task<IActionResult> ChangeUserPassword(ChangeUserPasswordDto dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(dto.UserId);
                if (user == null)
                    return HandleResult(Result<string>.Failure(-400, "کاربر پیدا نشد"));
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, dto.NewPassword);
                return HandleResult(Result<string>.Success(200, "عملیات با موفقیت انجام شد"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return HandleResult(Result<string>.Failure(-1400, "مشکلی رخ داده است"));
            }
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> AddProfilePhoto([FromForm] UpdateUserProfilePhotoDto dto, [FromRoute] string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
                return BadRequest(Result<string>.Failure(-404, "کاربر پیدا نشد"));
            string path = null;
            string fileName = null;
            string base64 = null;

            if (dto?.ProfilePhotoFile?.Length > 0)
            {
                var extension = Path.GetExtension(dto.ProfilePhotoFile.FileName);
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

                if (!allowedExtensions.Contains(extension))
                {
                    return HandleResult(Result<string>.Failure(-400, "فرمت تصویر پروفایل مجاز نمی باشد"));
                }

                path = Path.Combine(_environment.ContentRootPath, "uploads", "userPhotos");
                fileName = Guid.NewGuid() + extension;
                var filePath = Path.Combine(path, fileName);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                await using var fileStream = new FileStream(filePath, FileMode.Create);
                var readStream = dto.ProfilePhotoFile.OpenReadStream();

                Image.FromStream(readStream)
                    .ResizeImage(850)
                    .SetResolution(72, 72)
                    .Save(fileStream, ImageFormat.Jpeg);

                await fileStream.DisposeAsync();

                var imageArray = await System.IO.File.ReadAllBytesAsync(filePath);
                base64 = Convert.ToBase64String(imageArray);
            }

            //var command = new UpdateUserProfilePhoto.Command(user.Id, filePath, fileName);
            var anyImage = await _context.UserImages.AnyAsync(x => x.UserId == userId);
            _context.UserImages.Add(new UserImage
            {
                FileName = fileName,
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                IsMainPhoto = !anyImage
            });
            await _context.SaveChangesAsync();
            return HandleResult(Result<string>.Success(200, "عملیات با موفقیت انجام شد", base64));
        }

        [HttpPut("{userImageId}")]
        public async Task<IActionResult> ChangeMainPhoto(string userImageId)
        {
            var userImage = await _context.UserImages.FirstOrDefaultAsync(x => x.Id == userImageId);
            if (userImage == null)
                return BadRequest(Result<string>.Failure(-404, "عکس پیدا نشد"));

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userImage.UserId);
            if (user == null)
                return BadRequest(Result<string>.Failure(-404, "کاربر پیدا نشد"));

            var userImages = await _context.UserImages
                .Where(x => x.UserId == user.Id && x.Id != userImageId)
                .ToListAsync();
            foreach (var x in userImages)
            {
                x.IsMainPhoto = false;
                _context.UserImages.Update(x);
            }
            userImage.IsMainPhoto = true;
            _context.UserImages.Update(userImage);
            await _context.SaveChangesAsync();
            return HandleResult(Result<string>.Success(200, "عملیات با موفقیت انجام شد"));
        }

        [HttpDelete("{userImageId}")]
        public async Task<IActionResult> DeletePhoto(string userImageId)
        {
            var userImage = await _context.UserImages.FirstOrDefaultAsync(x => x.Id == userImageId);
            if (userImage == null)
            {
                return BadRequest(Result<string>.Failure(-404, "عکس پیدا نشد"));
            }
            var path = Path.Combine(_environment.ContentRootPath, "uploads", "userPhotos", userImage.FileName);
            //string FileName = "test.txt";
            //string Path = "E:\\" + FileName;
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }
            _context.UserImages.Remove(userImage);
            await _context.SaveChangesAsync();
            return HandleResult(Result<string>.Success(200, "عملیات با موفقیت انجام شد"));
        }
    }
}

