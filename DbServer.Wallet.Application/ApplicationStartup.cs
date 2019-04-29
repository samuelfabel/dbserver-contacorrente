using DbServer.Wallet.Application.Data.Repositories;
using DbServer.Wallet.Application.Data.Repositories.Common;
using DbServer.Wallet.Application.Data.Repositories.Interfaces;
using DbServer.Wallet.Application.Migration;
using DbServer.Wallet.Application.Security;
using DbServer.Wallet.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DbServer.Wallet.Application
{
    public static class ApplicationStartup
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            CreateDataBaseMigration.Run();

            services.AddScoped<SqlConnectionProvider>();

            //Repositories
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IBalanceRepository, BalanceRepository>();
            services.AddScoped<IStatementRepository, StatementRepository>();

            //Services
            services.AddScoped<AccountService>();
            services.AddScoped<WalletService>();

            //Security
            services.AddScoped<PasswordSecurity>();
        }
    }
}