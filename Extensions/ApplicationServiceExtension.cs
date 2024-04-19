using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Project.ProjectDataBase;
using Services.UserAccessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,IConfiguration config)
        {
            services.AddDbContext<DataContext>(opt =>
            {
            //opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));//,
            //opt.UseSqlServer("Data Source=.\\MSSQLSERVER2019;Integrated Security=False;User ID=korandd;Password=UI9hy8uj;Connect Timeout=15;Encrypt=False;Packet Size=4096");//,
            //opt.UseSqlServer("Server=.;Database=securety_;User Id=korandd;Password=UI9hy8uj;");//,
                    //b => b.MigrationsAssembly(typeof(DataContext).Assembly.FullName));
                
                //opt.UseSqlServer("Server=.;Database=Tester;User Id=mrb;Password=123123;",
                   // b => b.MigrationsAssembly(typeof(DataContext).Assembly.FullName));
            });
            //services.AddCors(opt =>
            //{
            //    opt.AddPolicy("CorsPolicy", policy =>
            //    {
            //        policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
            //    });
            //});
            services.AddScoped<IUserAccessor, UserAccessor>();
           // services.AddTransient<ICaptchaService, CaptchaService>();
            return services;
        }
    }
}
