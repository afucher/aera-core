using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using aera_core.Controllers;
using aera_core.Persistencia;
using aera_core.POUIHelpers;
using AeraIntegrationTest.Builders;
using FluentAssertions;
using NUnit.Framework;
using BCrypt.Net;

namespace AeraIntegrationTest
{
    public class PagamentosControllerTeste : BaseTesteApi
    {
        [Test]
        public async Task DeveRetornarPagamentosPorMatricula()
        {
            var pagamento = PagamentoBuilder.ParaMatricula(1).Generate();
            var pagamentoCriado = _contextoParaTestes.GravaPagamento(pagamento).Entity;

            var resposta = await _httpClient.GetAsync("/api/pagamentos/matricula/1");

            resposta.Should().HaveHttpStatusCode(HttpStatusCode.OK).And
                .Satisfy<IReadOnlyCollection<PagamentoDTO>>(model =>
                {
                    model.Should().BeEquivalentTo(
                        new
                        {
                            Valor = pagamentoCriado.Value,
                            Parcela = pagamentoCriado.Installment,
                            TotalDeParcelas = pagamentoCriado.NumberInstallments,
                            Pago = pagamentoCriado.Paid.GetValueOrDefault(false),
                            NomeAluno = pagamentoCriado.TurmaAluno?.Cliente.name,
                            IdMatricula = pagamentoCriado.ClientGroupId,
                            DataDeVencimento = pagamentoCriado.DueDate?.ToString("yyyy-MM-dd")
                        }
                    );
                });
        }
        
        [Test]
        public async Task DeveRetornarListaVaziaQuandoNãoHouveremPagamentosParaMatricula()
        {
            var pagamento = PagamentoBuilder.ParaMatricula(1).Generate();
            _contextoParaTestes.GravaPagamento(pagamento);

            var resposta = await _httpClient.GetAsync("/api/pagamentos/matricula/11");

            resposta.Should().HaveHttpStatusCode(HttpStatusCode.OK).And.BeAs(new PagamentoDTO[] {} );
        }
    }
}