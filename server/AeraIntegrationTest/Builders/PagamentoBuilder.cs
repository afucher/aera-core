using aera_core;
using aera_core.Models;
using aera_core.Persistencia;
using Bogus;

namespace AeraIntegrationTest.Builders
{
    public class PagamentoBuilder
    {
        static Faker<PagamentoDB> _faker => new Faker<PagamentoDB>()
            .RuleFor(p => p.Installment, f => f.Random.Number());

        public static Faker<PagamentoDB> ParaMatricula(int matricula) => _faker
            .RuleFor(p => p.ClientGroupId, _ => matricula);
    }
}