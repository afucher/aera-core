using System.Net.Http;
using NUnit.Framework;

namespace AeraIntegrationTest
{
    public class BaseTesteApi
    {
        protected HttpClient _httpClient;

        [SetUp]
        public void ConfiguraHttpClient()
        {
            _httpClient = AmbienteDeTestes.Factory.CreateClient();
        }
    }
}