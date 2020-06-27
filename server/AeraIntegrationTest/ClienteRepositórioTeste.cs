using System.Threading.Tasks;
using aera_core.Domain;
using aera_core.Persistencia;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Respawn;

namespace AeraIntegrationTest
{
    public class ClienteRepositórioTeste
    {
        private static string parametrosConexão =
            "Server=localhost;Port=5432;User Id=postgres;Password=root;Database=aera_test";
        private ClientesContexto contexto;
        Checkpoint _checkpoint = new Checkpoint
        {
            SchemasToInclude = new []
            {
                "public"
            },
            DbAdapter = DbAdapter.Postgres
        };

        [SetUp]
        public async Task Setup()
        {
            contexto = new ClientesContexto(new DbContextOptionsBuilder()
                .UseNpgsql(parametrosConexão)
                .Options);
            contexto.Database.OpenConnection();
            await _checkpoint.Reset(contexto.Database.GetDbConnection());
        }

        [Test]
        public void DeveRetornarListaDeClientesVazias()
        {
            var repositório = new ClienteRepositório(contexto);

            var clientes = repositório.ObterClientes();

            clientes.Should().BeEmpty();
        }

        [Test]
        public void DeveRetornarClientesJáExistentes()
        {
            contexto.Clientes.Add(new ClienteDB
            {
                name = "Arthur",
                cpf = "12345678933",
                email = "a@a.com.br"
            });
            contexto.SaveChanges();
            var repositório = new ClienteRepositório(contexto);

            var clientes = repositório.ObterClientes();

            clientes.Should().BeEquivalentTo(new Cliente
            {
                Nome = "Arthur",
                CPF = "12345678933",
                Email = "a@a.com.br"
            });
        }
    }
}