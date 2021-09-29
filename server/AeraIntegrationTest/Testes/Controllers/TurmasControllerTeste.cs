using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using aera_core.Controllers;
using aera_core.Models;
using aera_core.Persistencia;
using aera_core.POUIHelpers;
using AeraIntegrationTest.Builders;
using FluentAssertions;
using NUnit.Framework;
using BCrypt.Net;

namespace AeraIntegrationTest
{
    public class TurmasControllerTeste : BaseTesteApi
    {
        [Test]
        public async Task DeveGerarQuatroPagamentos_DoisParaCadaAluno()
        {
            var turma = new TurmaDBBuilder().Generate();
            turma.Alunos = new ClienteDBBuilder().Generate(2);
            var turmaCriada = _contextoParaTestes.GravaTurma(turma);

            var resposta =
                await _httpClient.PostAsync($"/api/turmas/{turmaCriada.Entity.id}/pagamentos?parcelas=2&valor=351.50&dataVencimento=2021-05-04", new StringContent(""));

            resposta.Should()
                .Satisfy<TurmaDTO>( model =>
                    {
                        model.Id.Should().Be( turmaCriada.Entity.id );
                    });
            _contextoParaTestes.Pagamentos.Count().Should().Be(4);
        }
        
        [Test]
        public async Task DeveGerarPagamentosApenasParaAlunosQueNãoTemPagamentoDaTurma()
        {
            var turma = new TurmaDBBuilder().Generate();
            var alunos = new ClienteDBBuilder().Generate(2);
            turma.Alunos = alunos;
            var turmaCriada = _contextoParaTestes.GravaTurma(turma);
            var matricula = _contextoParaTestes.Matriculas.First(m => m.ClienteId == alunos[0].id);
            matricula.Pagamentos = PagamentoBuilder.ParaMatricula(matricula.id).GerarParcelas(1);
            _contextoParaTestes.SaveChanges();

            var resposta =
                await _httpClient.PostAsync($"/api/turmas/{turmaCriada.Entity.id}/pagamentos?parcelas=2&valor=351.50&dataVencimento=2021-05-04", new StringContent(""));

            resposta.Should()
                .Satisfy<TurmaDTO>( model =>
                    {
                        model.Id.Should().Be( turmaCriada.Entity.id );
                    });
            _contextoParaTestes.Pagamentos.Count().Should().Be(3);
        }
    }
}