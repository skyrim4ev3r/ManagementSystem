using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.Core;
using Project.DTOs.Users.Users;
using Project.ProjectDataBase;
using Project.ProjectDataBase.Domain;
using Services.UserAccessors;
using System;
using System.Threading.Tasks;

namespace Project.Controllers.Users
{
    [ControllerName("Users: User")]
    [Route("Users/[controller]/[action]")]
    [Authorize]
    public class UserController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserAccessor _userAccessor;

        public UserController(DataContext context, ILogger<UserController> logger,
            UserManager<AppUser> userManager, IUserAccessor userAccessor)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _userAccessor = userAccessor;
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(_userAccessor.GetUserId());
                if (user == null)
                    return BadRequest(Result<string>.Success(-400, ""));

                var signInResult = await _userManager.CheckPasswordAsync(user, dto.OldPassword);
                if (!signInResult)
                    return HandleResult(Result<string>.Failure(-400, "رمز عبور اشتباه میباشد"));
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, dto.Password);
                return Ok(Result<string>.Success(200, "پسورد تغییر یافت"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(Result<string>.Failure(-1400, "عملیات با شکست مواجه شد"));
            }
        }    
    }
}

