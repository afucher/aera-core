using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AeraIntegrationTest
{
    public class AccessToken
    {
        public string access_token;
    }
    public static class Helpers
    {
        public static async Task<string> obterAccessToken(HttpClient httpClient)
        {
            var response = await httpClient.PostAsJsonAsync("/api/autenticacao/login", "");
            var token = await response.Content.ReadAsAsync<AccessToken>();
            return token.access_token;
        }
    }
}