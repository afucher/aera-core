using System;
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

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _factory.Dispose();
        }
        
        [Test]
        public async Task RetornaClientes()
        {
            var clientes = JsonSerializer.Serialize(new POUIListResponse<ClienteDTO>(new []{new ClienteDTO
            {
                id = 1
            }}, true));
            var resposta = await _client.GetAsync("/api/clientes?pageSize=10");
            var conteúdo = await resposta.Content.ReadAsStringAsync();
            conteúdo.Should().Be(clientes);
        }
        
        
        [Test]
        public async Task NãoRetornaClientesQuandoPaginaçãoForMaiorQueQuantidadeDeRegistros()
        {
            var clientes = JsonSerializer.Serialize(new POUIListResponse<ClienteDTO>(ArraySegment<ClienteDTO>.Empty));
            var resposta = await _client.GetAsync("/api/clientes?page=2&pageSize=10");
            resposta.StatusCode.Should().Be(HttpStatusCode.OK);

            var conteúdo = await resposta.Content.ReadAsStringAsync();
            conteúdo.Should().Be(clientes);
        }
    }
}