using aera_core;
using aera_core.Persistencia;
using Bogus;

namespace AeraIntegrationTest.Builders
{
    public class UsuarioBuilder
    {
        static Faker<Usuário> _faker => new Faker<Usuário>()
              .RuleFor(p => p.Username, f => f.Name.FirstName());
        public static Usuário Gerar(string senha) => _faker
          .RuleFor(p => p.Password, _ => BCrypt.Net.BCrypt.HashPassword(senha))
          .Generate();
    }
}