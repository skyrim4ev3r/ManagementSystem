using Project.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Extensions
{
    public static class AutoCreateRolesExtension
    {
        public static IHost AutoCreateRoles(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    if (!roleManager.RoleExistsAsync(Roles.Collegian).Result)
                    {
                        roleManager.CreateAsync(new IdentityRole(Roles.Collegian)).Wait();
                    }
                    if (!roleManager.RoleExistsAsync(Roles.Admin).Result)
                    {
                        roleManager.CreateAsync(new IdentityRole(Roles.Admin)).Wait();
                    }                    
                    if (!roleManager.RoleExistsAsync(Roles.Master).Result)
                    {
                        roleManager.CreateAsync(new IdentityRole(Roles.Master)).Wait();
                    } 
                    if (!roleManager.RoleExistsAsync(Roles.InternshipLocation).Result)
                    {
                        roleManager.CreateAsync(new IdentityRole(Roles.InternshipLocation)).Wait();
                    }
                }
                catch (Exception e)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "Error During Creating Roles");
                }
            }
            return host;
        }
    }
}
