using ClaimServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Project.ProjectDataBase;
using Project.ProjectDataBase.Domain;
using Project.Services.TokenServices;
using Project.Services.TokenServices.Interfaces;
using System;
using System.Text;

namespace Project.Extensions
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
            IConfiguration config)
        {

            services.AddIdentityCore<AppUser>(options =>
            {
                #region Password

                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 2;
                options.Password.RequiredLength = 6;

                #endregion

                #region Lockout

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);

                #endregion

                #region User

                options.User.RequireUniqueEmail = false;
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-";

                #endregion
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DataContext>()
                .AddSignInManager<SignInManager<AppUser>>()
                .AddUserManager<UserManager<AppUser>>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddPersianIdentityErrorDescriber()
                .AddDefaultTokenProviders();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//Microsoft.AspNetCore.Authentication.JwtBearer
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true
                    };
                });
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ClaimService>();
            return services;
        }
    }
}
