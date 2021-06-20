using System.Collections.Generic;
using System.Collections.Immutable;
using aera_core.Persistencia;
using Bogus;

namespace AeraIntegrationTest.Builders
{
    public static class ProfessorDBBuilder
    {
        public static ClienteDB Generate()
        {
            var professorFaker = new Faker<ClienteDB>()
                .RuleFor(p => p.name, f => f.Name.FullName())
                .RuleFor(p => p.teacher, _ => true);
            return professorFaker.Generate();
        }
    }
}