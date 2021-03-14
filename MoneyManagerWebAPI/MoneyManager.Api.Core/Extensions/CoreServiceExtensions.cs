using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MoneyManager.Api.Core.Mappings;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MoneyManager.Api.Core.Extensions
{
    public static class CoreServiceExtensions
    {
        public static void AddApplicationCoreLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(GeneralProfile));
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
