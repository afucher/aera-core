using System;
using System.Text.Json;
using System.Threading.Tasks;
using aera_core.Controllers;
using aera_core.Persistencia;
using aera_core.POUIHelpers;
using AeraIntegrationTest.Builders;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using JsonSerializer = System.Text.Json.JsonSerializer;
using FluentAssertions.Json;

namespace AeraIntegrationTest
{
    public class ProfessoresControllerTeste : BaseTesteApi
    {
        [Test]
        public async Task DeveRetornarProfessorComCamposCorretos()
        {
            var professor = ProfessorDBBuilder.Generate();
            var professorCriado = _contextoParaTestes.GravaProfessor(professor);

            var resposta = await _httpClient.GetAsync("/api/professores?pageSize=10");

            resposta.Should().SatisfyJTokenContent(
                new
                {
                    items = new[]
                    {
                        new {professorCriado.Entity.id, nome = professorCriado.Entity.name}
                    },
                    hasNext = false
                }.SerializarComoJToken()
            );
        }

        [Test]
        public async Task DeveRetornarDizendoQueTemMaisItensEQuantidadeIgualAoPageSize()
        {
            var professores = ProfessorDBBuilder.Generate(11);
            _contextoParaTestes.GravaProfessores(professores);

            var resposta = await _httpClient.GetAsync("/api/professores?pageSize=10");

            resposta.Should()
                .Satisfy<POUIListResponse<ProfessorDTO>>(model =>
                {
                    model.hasNext.Should().BeTrue();
                    model.items.Should().HaveCount(10);
                });
        }
    }
}