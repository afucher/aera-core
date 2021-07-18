using aera_core.Domain;
using AeraIntegrationTest.Builders;
using FluentAssertions;
using NUnit.Framework;

namespace AeraIntegrationTest
{
    public class PagamentoRepositórioTeste : BaseTesteBanco
    {
        [Test]
        public void ObterPorMatricula_DeveRetornarListaVaziasQuandoNãoHouverPagamentos()
        {
            var repositório = GetService<IPagamentosPort>();
           
            var pagamentos = repositório.ObterPorMatricula(1);

            pagamentos.Should().BeEmpty();
        }
        
        [Test]
        public void ObterPorMatricula_DeveRetornarListaVazia_QuandoMatriculaForDiferente()
        {
            _contextoParaTestes.Pagamentos.Add(PagamentoBuilder.ParaMatricula(2).Generate());
            _contextoParaTestes.SaveChanges();
            var repositório = GetService<IPagamentosPort>();
           
            var pagamentos = repositório.ObterPorMatricula(1);

            pagamentos.Should().BeEmpty();
        }
        
        [Test]
        public void ObterPorMatricula_DeveRetornarListaDePagamentos()
        {
            var pagamentosBanco = PagamentoBuilder.ParaMatricula(2).Generate(2);
            _contextoParaTestes.Pagamentos.AddRange(pagamentosBanco);
            _contextoParaTestes.SaveChanges();
            var repositório = GetService<IPagamentosPort>();
           
            var pagamentos = repositório.ObterPorMatricula(2);

            pagamentos.Should().BeEquivalentTo(pagamentosBanco);
        }
    }
}