using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Core;
using Project.DTOs.Auth;
using Project.ProjectDataBase;
using Project.ProjectDataBase.Domain;
using Project.Services.TokenServices.Interfaces;
using Project.Services.TokenServices.Models;
using System;
using System.Threading.Tasks;

namespace Project.Controllers.Publics
{
    [ControllerName("Public: Auth")]
    [Route("Public/[controller]/[action]")]
    public class AuthController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public AuthController(DataContext context, UserManager<AppUser> userManager
            , ITokenService tokenService)//, ILogger<>)
        {
            _context = context;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginDto dto)
        {
            var captcha = await _context.CaptchaCodes.FirstOrDefaultAsync(x => x.Id == dto.CaptchaId);
            if(captcha == null || captcha.ExpireAt < DateTime.Now)
                return HandleResult(Result<string>.Failure(-400, "کپچا معتبر نیست"));
            if(captcha.Code != dto.CaptchaCode)
            {
                _context.CaptchaCodes.Remove(captcha);
                await _context.SaveChangesAsync();
                return HandleResult(Result<string>.Failure(-400, "کپچا معتبر نیست"));
            }

            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
                return HandleResult(Result<string>.Failure(-400, "نام کاربری یا رمز عبور اشتباه میباشد"));

            var signInResult = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!signInResult)
                return HandleResult(Result<string>.Failure(-400, "نام کاربری یا رمز عبور اشتباه میباشد"));

            if (user.IsDeleted)
            {
                return HandleResult(Result<string>.Failure(-400, $"نام کاربری {dto.UserName} غیر فعال شده است جهت فعال سازی با پشتیبانی تماس بگیرید"));
            }

            var tokenResult = await _tokenService.CreateTokenResult(user);
            return Ok(Result<TokenResult>
                .Success(200, "با موفقیت وارد حساب کاربری خود شدید",
                tokenResult));
        }
        //[HttpPost]
        //public async Task<IActionResult> SignInTesterwewewew(LoginDto dto)
        //{
        //    var user = await _userManager.FindByNameAsync(dto.UserName);
        //    if (user == null)
        //        return HandleResult(Result<string>.Failure(-400, "نام کاربری یا رمز عبور اشتباه میباشد"));

        //    var signInResult = await _userManager.CheckPasswordAsync(user, dto.Password);
        //    if (!signInResult)
        //        return HandleResult(Result<string>.Failure(-400, "نام کاربری یا رمز عبور اشتباه میباشد"));

        //    if (user.IsDeleted)
        //    {
        //        return HandleResult(Result<string>.Failure(-400, $"نام کاربری {dto.UserName} غیر فعال شده است جهت فعال سازی با پشتیبانی تماس بگیرید"));
        //    }

        //    var tokenResult = await _tokenService.CreateTokenResult(user);
        //    return Ok(Result<TokenResult>
        //        .Success(200, "با موفقیت وارد حساب کاربری خود شدید",
        //        tokenResult));
        //}

        [HttpPost]
        public async Task<IActionResult> RefreshToken(RefreshTokenInputDto dto)
        {
            var rt = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == dto.RefreshToken);
            if (rt == null || rt.ExpireAt < DateTime.Now)
                return BadRequest("توکن معتبر نیست");
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == rt.UserId);

            if (user == null) return BadRequest();
            if (user.IsDeleted) return BadRequest();
            var tokenResult = await _tokenService.CreateTokenResult(user);

            _context.RefreshTokens.Remove(rt);
            await _context.SaveChangesAsync();
            return Ok(Result<TokenResult>.Success(200, "عملیات با موفقیت انجام شد", tokenResult));
        }
    }
}
