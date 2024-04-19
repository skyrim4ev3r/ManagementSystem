using Project.ProjectDataBase.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Project.Core;
using Project.ProjectDataBase;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Project.Services.TokenServices.Interfaces;
using Project.Services.TokenServices.Models;
using Microsoft.Extensions.Logging;
using ClaimServices;

namespace Project.Services.TokenServices
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<TokenService> _logger;
        private readonly ClaimService ClaimService;

        public TokenService(IConfiguration config, DataContext context,
            UserManager<AppUser> userManager
            , RoleManager<IdentityRole> roleManager, ILogger<TokenService> logger, ClaimService claimService)
        {
            _config = config;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            ClaimService = claimService;
        }
        public string CreateAccessToken(AppUser user, string refreshTokenId)
        {
            var c = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, _userManager.GetRolesAsync(user).Result?.First())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Subject = new ClaimsIdentity(ClaimService.Creator(user.UserName, user.Id, _userManager.GetRolesAsync(user).Result?.First())),
                Subject = new ClaimsIdentity(

                    c
                    //ClaimService.Creator(user.UserName, user.Id, _userManager.GetRolesAsync(user).Result?.First())
                    
                    ),
                Expires = DateTime.Now.AddMinutes(1200),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public RefreshToken CreateRefreshToken(AppUser user)
        {
            string refreshToken;
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken = Convert.ToBase64String(randomNumber);
            }
            refreshToken += Guid.NewGuid().ToString();

            return new RefreshToken
            {
                Id = Guid.NewGuid().ToString(),
                ExpireAt = DateTime.Now.AddHours(100),
                CreatedAt = DateTime.Now,
                Token = refreshToken,
                User = user
            };
        }        

        public async Task<TokenResult> CreateTokenResult(AppUser user)
        {
            var refreshTokenFullObject = CreateRefreshToken(user);
            try
            {
                await _context.RefreshTokens.AddAsync(refreshTokenFullObject);
                await _context.SaveChangesAsync();
            }
            catch
            {
                _logger.LogCritical("مشکلی در ذخیره سازی رفرش توکن رخ داده است");
            }

            var accessToken = CreateAccessToken(user, refreshTokenFullObject.Id);

            var tokenResult = new TokenResult
            {
                RefreshToken = refreshTokenFullObject.Token,
                AccessToken = accessToken
            };

            return tokenResult;
        }
       
    }
}
