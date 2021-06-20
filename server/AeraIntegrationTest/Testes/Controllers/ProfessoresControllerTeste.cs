using System.Text.Json;
using System.Threading.Tasks;
using aera_core.Controllers;
using aera_core.Persistencia;
using aera_core.POUIHelpers;
using AeraIntegrationTest.Builders;
using FluentAssertions;
using NUnit.Framework;

namespace AeraIntegrationTest
{
    public class ProfessoresControllerTeste : BaseTesteApi
    {
        [Test]
        public async Task RetornaProfessores()
        {
            var professor = ProfessorDBBuilder.Generate();
            var professorCriado = _contextoParaTestes.Clientes.Add(professor);
            _contextoParaTestes.SaveChanges();
            
            var resposta = await _httpClient.GetAsync("/api/professores?pageSize=10");

            resposta.Should()
                .Satisfy<POUIListResponse<ProfessorDTO>>( model =>
                    {
                        model.hasNext.Should().BeFalse();
                        model.items.Should().BeEquivalentTo(
                            new { professorCriado.Entity.id, nome = professorCriado.Entity.name }
                        );
                    });
        }
    }
}