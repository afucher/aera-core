using System;
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
            var cliente = new ClienteDB
            {
                name = "Arthur",
                cpf = "12345678933",
                email = "a@a.com.br",
                phone = "551155555555",
                teacher = false,
                cel_phone = "5511955555555",
                com_phone = "551155555555",
                address1 = "Rua tal, numero 343",
                address2 = "complemento 1",
                address3 = "mais um complemento",
                city = "São Paulo",
                state = "São Paulo",
                zip_code = "00000000",
                profession = "Profissional",
                edu_lvl = "3C",
                old_code = "O5000",
                birth_date = DateTime.Today,
                birth_hour = new TimeSpan(11, 50, 0),
                birth_place = "São Paulo/SP",
                note = "Comentários sobre cliente"
            };
            var entityEntry = contexto.Clientes.Add(cliente);
            contexto.SaveChanges();
            entityEntry.State = EntityState.Detached;
            var repositório = new ClienteRepositório(contexto);

            var clientes = repositório.ObterClientes();

            clientes.Should().BeEquivalentTo(new Cliente
            {
                Id = entityEntry.Entity.id,
                Nome = "Arthur",
                CPF = "12345678933",
                Email = "a@a.com.br",
                Telefone = "551155555555",
                ÉProfessor = false,
                Celular = "5511955555555",
                TelefoneComercial = "551155555555",
                address1 = "Rua tal, numero 343",
                address2 = "complemento 1",
                address3 = "mais um complemento",
                Cidade = "São Paulo",
                Estado = "São Paulo",
                CEP = "00000000",
                Profissão = "Profissional",
                NívelEducação = "3C",
                CódigoAntigo = "O5000",
                DataNascimento = DateTime.Today,
                HorárioNascimento = new TimeSpan(11, 50, 0),
                LocalNascimento = "São Paulo/SP",
                Observação = "Comentários sobre cliente"
            });
        }
    }
}