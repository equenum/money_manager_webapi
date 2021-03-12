using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MoneyManager.Api.Core.Mappings;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MoneyManager.Api.Core
{
    public static class ServiceExtensions
    {
        public static void AddApplicationCoreLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(GeneralProfile));
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
