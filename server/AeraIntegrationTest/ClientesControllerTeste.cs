using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using aera_core.Controllers;
using aera_core.Domain;
using aera_core.POUIHelpers;
using FluentAssertions;
using NUnit.Framework;

namespace AeraIntegrationTest
{
    public class ClientesControllerTeste
    {
        private APIWebApplicationFactory _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void GivenARequestToTheController()
        {
            _factory = new APIWebApplicationFactory();
            _client = _factory.CreateClient();
        }
        
        [Test]
        public async Task RetornaAlohaMundoNaRotaPadrão()
        {
            var clientes = JsonSerializer.Serialize(new POUIListResponse<ClienteDTO>(new []{new ClienteDTO
            {
                id = 1
            }}));
            var result = await _client.GetAsync("/api/clientes");
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Content.ReadAsStringAsync().Result.Should().Be(clientes);
        }
    }
}