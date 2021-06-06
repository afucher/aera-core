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
    public class ProfessoresControllerTeste : BaseTesteApi
    {
        [Test]
        public async Task RetornaClientes()
        {
            var contexto = AmbienteDeTestes.Factory.Services.CreateScope().ServiceProvider
                .GetService<AplicaçãoContexto>();
            var professor = new ClienteDB
            {
                name = "Nome1",
                teacher = true
                
            };
            var professorCriado = contexto.Clientes.Add(professor);
            contexto.SaveChanges();
            var professorDTO = new ProfessorDTO
            {
                id = professorCriado.Entity.id,
                nome = "Nome1"
            };
            var professores = JsonSerializer.Serialize(new POUIListResponse<ProfessorDTO>(new []{professorDTO}));
            var resposta = await _httpClient.GetAsync("/api/professores?pageSize=10");
            var conteúdo = await resposta.Content.ReadAsStringAsync();
            conteúdo.Should().Be(professores);
        }
    }
}