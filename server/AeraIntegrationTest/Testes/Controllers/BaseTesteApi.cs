using System.Net.Http;
using System.Net.Http.Headers;
using aera_core;
using aera_core.Domain;
using NUnit.Framework;

namespace AeraIntegrationTest
{
    public class BaseTesteApi : BaseTesteBanco
    {
        protected HttpClient _httpClient;
        protected string _token;
        protected Usuário usuarioLogado;

        [SetUp]
        public void ConfiguraHttpClient()
        {
            _httpClient = AmbienteDeTestes.Factory.CreateClient();
            usuarioLogado = new Usuário {Username = "usuarioTeste", Password = "senhaTeste"};
            GetService<IUsuárioPort>().Criar(usuarioLogado);
            _token = Helpers.ObterAccessToken(_httpClient, "usuarioTeste", "senhaTeste").Result;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }
    }
}