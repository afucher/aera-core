using System.Net.Http;
using aera_core.Persistencia;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace AeraIntegrationTest
{
    public class BaseTesteApi : BaseTesteBanco
    {
        protected HttpClient _httpClient;

        [SetUp]
        public void ConfiguraHttpClient()
        {
            _httpClient = AmbienteDeTestes.Factory.CreateClient();
        }
    }
}