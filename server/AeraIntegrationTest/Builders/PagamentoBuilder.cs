using System.Collections.Generic;
using aera_core;
using aera_core.Models;
using aera_core.Persistencia;
using Bogus;

namespace AeraIntegrationTest.Builders
{
    public static class PagamentoBuilder
    {
        static Faker<PagamentoDB> _faker => new Faker<PagamentoDB>()
            .RuleFor(p => p.Installment, f => f.Random.Number());

        public static Faker<PagamentoDB> ParaMatricula(int matricula) => _faker
            .RuleFor(p => p.ClientGroupId, _ => matricula);

        public static List<PagamentoDB> GerarParcelas(this Faker<PagamentoDB> faker, int numeroDeParcelas)
        {
            var parcela = 1;
            return faker
                .RuleFor(p => p.Installment, _ => parcela++)
                .Generate(numeroDeParcelas);
        }
    }
}