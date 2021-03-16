using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyManager.Api.Core.Interfaces;
using MoneyManager.Api.Core.Interfaces.Repositories;
using MoneyManager.Api.Infrastructure.Data.EntityFramework;
using MoneyManager.Api.Infrastructure.Data.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Api.Infrastructure.Extentions
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ICategoryRepositoryAsync, CategoryRepository>();
            services.AddTransient<ITransactionRepositoryAsync, TransactionRepository>();
            services.AddTransient<IUserRepositoryAsync, UserRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped(_ => new ApplicationDbContext());
        }
    }
}
