using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using aera_core;

namespace AeraIntegrationTest
{
    public class AccessToken
    {
        public string access_token;
    }
    public static class Helpers
    {
        public static async Task<string> ObterAccessToken(HttpClient httpClient, string usuario, string senha)
        {
            var response = await httpClient.PostAsJsonAsync("/api/autenticacao/login", new {usuario, senha});
            var token = await response.Content.ReadAsAsync<AccessToken>();
            return token.access_token;
        }
    }
}
