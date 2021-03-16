using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MoneyManager.Api.Core.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.Api.Extensions
{
    public static class AuthenticationExtensions
    {
        public static void AddAuthenticationExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors();
            var secretSettingsSection = configuration.GetSection("SecretSettings");
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
        }

        public static void UseAuthenticationExtensions(this IApplicationBuilder app)
        {
            app.UseCors(options =>
            {
                options.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });

            app.UseAuthentication();
        }
    }
}
