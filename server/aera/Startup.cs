using System;
using aera_core.Domain;
using aera_core.Persistencia;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace aera_core
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
            services.AddControllers();
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = ".";
            });
            // var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            // Console.WriteLine(">>>>>> " + databaseUrl);
            // var databaseUri = new Uri(databaseUrl);
            // var userInfo = databaseUri.UserInfo.Split(':');
            //
            // var builder = new NpgsqlConnectionStringBuilder
            // {
            //     Host = databaseUri.Host,
            //     Port = databaseUri.Port,
            //     Username = userInfo[0],
            //     Password = userInfo[1],
            //     Database = databaseUri.LocalPath.TrimStart('/')
            // };
            //
            // services.AddDbContext<AplicaçãoContexto>(options => 
            //     options.UseNpgsql(builder.ToString()));
            // return ;
            services.AddDbContext<AplicaçãoContexto>(options => 
                options.UseNpgsql(Configuration.GetConnectionString("default")));
            services.AddScoped(provider => new ClienteRepositório(provider.GetService<AplicaçãoContexto>()));
            services.AddScoped(provider => new ClientesServiço(provider.GetService<ClienteRepositório>()));
            services.AddScoped(provider => new TurmaRepositorio(provider.GetService<AplicaçãoContexto>()));
            services.AddScoped(provider => new TurmasServiço(provider.GetService<TurmaRepositorio>()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
               app.UseHttpsRedirection();
            }


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSpaStaticFiles();
            
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                if (env.IsDevelopment())
                {
                    spa.Options.SourcePath = "../../AeraWebApp";
                    spa.UseAngularCliServer(npmScript: "start");
                    spa.Options.StartupTimeout = TimeSpan.FromSeconds(120);
                }
            });   
        }
    }
}
