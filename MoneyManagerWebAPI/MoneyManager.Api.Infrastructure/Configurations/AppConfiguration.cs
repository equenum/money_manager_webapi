using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MoneyManager.Api.Infrastructure.Configurations
{
    public static class AppConfiguration
    {
        // TODO: Move it somewhere more appropriate
        public static string GetConnectionString()
        {
            var builder = GetConfigurationBuilder();
            var connectionString = builder.Build().GetValue<string>("DefaultConnection");

            return connectionString;
        }

        private static IConfigurationBuilder GetConfigurationBuilder()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings." +
                    $"{ Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();

            return builder;
        }
    }
}
