using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoneyManager.Api.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoneyManager.Api.Core.Extensions;
using MoneyManager.Api.Extensions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using MoneyManager.Api.Core.Domain.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            // TODO: Extract out of here later
            services.AddCors();
            var secretSettingsSection = Configuration.GetSection("SecretSettings");
            services.Configure<SecretSettings>(secretSettingsSection);

            var secretSettings = secretSettingsSection.Get<SecretSettings>();
            var key = Encoding.ASCII.GetBytes(secretSettings.Secret);

            services.AddAuthentication(options => 
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => 
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            // Extract down until this line

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

            // Extract
            app.UseCors(options => 
            {
                options.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });

            app.UseAuthentication();
            // Until here

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
