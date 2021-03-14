using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManager.Api.Extensions
{
    public static class SwaggerAppBuilderExtension
    {
        public static void UseSwaggerUIExtension(this IApplicationBuilder app, IApiVersionDescriptionProvider prov)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options => 
            {
                foreach (var desc in prov.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json",
                        desc.GroupName.ToUpperInvariant());
                }

                options.RoutePrefix = "";
            });
        }
    }
}
