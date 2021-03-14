using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManager.Api.Extensions
{
    public static class SwaggerServiceExtension
    {
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(options => 
            {
                options.SwaggerDoc("MoneyManagerOpenAPISpec",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Money Manager API",
                        Version = "1",
                        Description = "Web API for managing personal finance records."
                    });

                options.CustomSchemaIds(type => type.FullName);
                options.IncludeXmlComments(string.Format(@"{0}\MoneyManager.Api.xml", 
                                           AppDomain.CurrentDomain.BaseDirectory));
            });
        }
    }
}
