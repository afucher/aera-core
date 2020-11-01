using System;
using System.IO;
using System.Linq;
using aera_core;
using aera_core.Domain;
using aera_core.Persistencia;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace AeraIntegrationTest
{
    public class APIWebApplicationFactory : WebApplicationFactory<Startup>
    {
        public IClientesPort ClientesPort { get; private set; } 
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<AplicaçãoContexto>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }
                
                services.AddDbContext<AplicaçãoContexto>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database
                // context (ApplicationDbContext).
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<AplicaçãoContexto>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<APIWebApplicationFactory>>();

                    // Ensure the database is created.
                    db.Database.EnsureCreated();

                    try
                    {
                        // Seed the database with test data.
                        //Utilities.InitializeDbForTests(db);
                        db.Clientes.Add(new ClienteDB());
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            "database with test messages. Error: {Message}", ex.Message);
                    }
                }
                
                // Remove configuração da aplicação para teste.
                /*descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(ClientesServiço));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                ClientesPort = Substitute.For<IClientesPort>();
                services.AddScoped<ClientesServiço>(provider => new ClientesServiço(ClientesPort));*/
            });
        }
    }
}