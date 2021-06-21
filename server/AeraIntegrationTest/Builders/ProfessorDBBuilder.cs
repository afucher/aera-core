using System.Collections.Generic;
using aera_core.Persistencia;
using Bogus;

namespace AeraIntegrationTest.Builders
{
    public static class ProfessorDBBuilder
    {
        static Faker<ClienteDB> _faker => new Faker<ClienteDB>()
              .RuleFor(p => p.name, f => f.Name.FullName())
              .RuleFor(p => p.teacher, _ => true);
        public static ClienteDB Generate() => _faker.Generate();
        
        public static IReadOnlyCollection<ClienteDB> Generate(int count) => _faker.Generate(count);
    }
}