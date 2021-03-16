using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoneyManager.Api.Core.Extensions;
using MoneyManager.Api.Extensions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Text;
using MoneyManager.Api.Infrastructure.Extentions;

namespace MoneyManager.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationCoreLayer();
            services.AddPersistenceInfrastructure();
            services.AddSwaggerExtension();
            services.AddAuthenticationExtensions(Configuration);
            services.AddControllers();
            services.AddApiVersioningExtension();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
                                IWebHostEnvironment env, 
                                   IApiVersionDescriptionProvider prov)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwaggerUIExtension(prov);
            app.UseRouting();
            app.UseAuthenticationExtensions();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => 
            {
                endpoints.MapControllers();
            });
        }
    }
}
