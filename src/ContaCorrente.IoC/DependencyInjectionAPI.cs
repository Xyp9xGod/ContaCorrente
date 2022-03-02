using ContaCorrente.Application.Interfaces;
using ContaCorrente.Application.Mappings;
using ContaCorrente.Application.Services;
using ContaCorrente.Domain.Interfaces;
using ContaCorrente.Infra.Data.Context;
using ContaCorrente.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContaCorrente.IoC
{
    public static class DependencyInjectionAPI
    {
        public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services,
            IConfiguration configuration)
        {
            
            var host = configuration["DBHOST"];
            var port = configuration["DBPORT"];
            var password = configuration["DBPASSWORD"];
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySQL($"server={host}; database=WarrenDb; userid=root; pwd={password}; port={port};")
            );

            services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            services.AddScoped<IBankAccountService, BankAccountService>();
            services.AddScoped<ITransactionService, TransactionService>();

            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            return services;
        }
    }
}
