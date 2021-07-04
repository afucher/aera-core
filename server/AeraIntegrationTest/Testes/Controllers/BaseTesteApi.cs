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
        protected User usuarioLogado;

        [SetUp]
        public void ConfiguraHttpClient()
        {
            _httpClient = AmbienteDeTestes.Factory.CreateClient();
            usuarioLogado = new User {Username = "usuarioTeste", Password = "senhaTeste"};
            GetService<IUsuarioPort>().CriaUsuario(usuarioLogado);
            _token = Helpers.obterAccessToken(_httpClient, "usuarioTeste", "senhaTeste").Result;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }
    }
}