using System.Net.Http;
using System.Net.Http.Headers;
using NUnit.Framework;

namespace AeraIntegrationTest
{
    public class BaseTesteApi : BaseTesteBanco
    {
        protected HttpClient _httpClient;
        protected string _token;

        [SetUp]
        public void ConfiguraHttpClient()
        {
            _httpClient = AmbienteDeTestes.Factory.CreateClient();
            _token = Helpers.obterAccessToken(_httpClient).Result;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }
    }
}