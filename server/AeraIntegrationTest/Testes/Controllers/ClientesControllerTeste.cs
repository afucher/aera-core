using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using aera_core.Controllers;
using aera_core.Domain;
using aera_core.Persistencia;
using aera_core.POUIHelpers;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace AeraIntegrationTest
{
    public class ClientesControllerTeste : BaseTesteApi
    {
        [Test]
        public async Task RetornaClientes()
        {
            var contexto = AmbienteDeTestes.Factory.Services.CreateScope().ServiceProvider
                .GetService<AplicaçãoContexto>();
            var cliente = new ClienteDB(); 
            contexto.Clientes.Add(cliente);
            contexto.SaveChanges();
            var clienteDTO = ClienteDTO.DoModelo(cliente.ParaCliente());
            clienteDTO.turmas = null;
            var clientes = JsonSerializer.Serialize(new POUIListResponse<ClienteDTO>(new []{clienteDTO}, true));
            var resposta = await _httpClient.GetAsync("/api/clientes?pageSize=10");
            var conteúdo = await resposta.Content.ReadAsStringAsync();
            conteúdo.Should().Be(clientes);
        }
        
        [Test]
        public async Task NãoRetornaClientesQuandoPaginaçãoForMaiorQueQuantidadeDeRegistros()
        {
            var clientes = JsonSerializer.Serialize(new POUIListResponse<ClienteDTO>(ArraySegment<ClienteDTO>.Empty));
            var resposta = await _httpClient.GetAsync("/api/clientes?page=2&pageSize=10");
            resposta.StatusCode.Should().Be(HttpStatusCode.OK);

            var conteúdo = await resposta.Content.ReadAsStringAsync();
            conteúdo.Should().Be(clientes);
        }
    }
}