using System.Threading.Tasks;
using aera_core.Persistencia;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace AeraIntegrationTest
{
    public class BaseTesteBanco
    {
        protected static IServiceScope _scope;
        protected static AplicaçãoContexto _contextoParaTestes;
        protected static T GetService<T>() => _scope.ServiceProvider.GetService<T>();

        // Simula scope do request
        [SetUp]
        public void SetUpScope()
        {
            _scope = AmbienteDeTestes.Factory.Services.CreateScope();
            _contextoParaTestes = AmbienteDeTestes.Factory.Services.CreateScope().ServiceProvider
                .GetService<AplicaçãoContexto>();
        }

        // Dispose do scope do request
        [TearDown]
        public void TearDownScope()
        {
            _scope.Dispose();
            _contextoParaTestes.Database.EnsureDeleted();
        }
    }
}