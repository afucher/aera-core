using System;
using aera_core.Persistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace aera_core
{
    public static class ConfiguracaoDbContext
    {
        
        public static void ConfiguraDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            if (EstaUsandoHeroku())
            {
                ConfiguraHeroku(services);
            }
            else
            {
                services.AddDbContext<AplicaçãoContexto>(options => 
                    options.UseNpgsql(configuration.GetConnectionString("default")));
            }
        }

        private static bool EstaUsandoHeroku()
        {
            try
            {
                return Environment.GetEnvironmentVariable("DATABASE_URL") != "";
            }
            catch (ArgumentNullException _)
            {
                return false;
            }
        }

        private static void ConfiguraHeroku(IServiceCollection services)
        {
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');
            
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Prefer,
                TrustServerCertificate = true
            };
            
            services.AddDbContext<AplicaçãoContexto>(options => 
                options.UseNpgsql(builder.ToString()));
        }
    }
}