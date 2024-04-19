using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Project.Extensions
{
    public class RemoveSchemasFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {

            IDictionary<string, OpenApiSchema> _remove = swaggerDoc.Components.Schemas;
            foreach (KeyValuePair<string, OpenApiSchema> _item in _remove)
            {
                swaggerDoc.Components.Schemas.Remove(_item.Key);
            }
        }
    }
    public static class SwaggerServiceExtensions
    {

        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            //services.AddSwaggerGen(c =>
            //{
            // c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            //});
            services.AddSwaggerGen(c =>
            {
                //c.SwaggerDoc("v1", new Info { Title = "You api title", Version = "v1" });
                //c.DocumentFilter<RemoveSchemasFilter>();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                //c.CustomSchemaIds(x => x.GUID+" "+x.Name);
                c.CustomSchemaIds(x => Guid.NewGuid()+" "+x.Name);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                 //c.IncludeXmlComments(xmlPath);
            });
            return services;
        }
    }
}
