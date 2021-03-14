using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManager.Api.Extensions
{
    public static class SwaggerAppBuilderExtension
    {
        public static void UseSwaggerUIExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options => 
            {
                options.SwaggerEndpoint("/swagger/MoneyManagerOpenAPISpec/swagger.json",
                                        "Money Manager API");
               
                options.RoutePrefix = "";
            });
        }
    }
}
