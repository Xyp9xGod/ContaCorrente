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
            //executar usando docker-compose.
            //var host = configuration["DBHOST"] ?? "database";
            //var host = configuration["DBHOST"] ?? "localhost";
            //var port = configuration["DBPORT"] ?? "3306";
            //var password = configuration["DBPASSWORD"] ?? "root";

            var host = configuration["DBHOST"];
            var port = configuration["DBPORT"];
            var password = configuration["DBPASSWORD"];

            services.AddDbContext<ApplicationDbContext>(options =>
                //options.UseMySQL(configuration.GetConnectionString("DefaultConnection"))
                options.UseMySQL($"server={host}; database=warrendb; userid=root; pwd={password}; port={port};")
            );

            //registro dos repositorios
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            //registro dos servicos
            services.AddScoped<IBankAccountService, BankAccountService>();
            services.AddScoped<ITransactionService, TransactionService>();

            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            return services;
        }
    }
}
