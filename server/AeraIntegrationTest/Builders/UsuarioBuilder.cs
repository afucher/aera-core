using aera_core;
using aera_core.Persistencia;
using Bogus;

namespace AeraIntegrationTest.Builders
{
    public class UsuarioBuilder
    {
        static Faker<User> _faker => new Faker<User>()
              .RuleFor(p => p.Password, _ => BCrypt.Net.BCrypt.HashPassword("senha"))
              .RuleFor(p => p.Username, f => f.Name.FirstName());
        public static User Generate() => _faker.Generate();
    }
}