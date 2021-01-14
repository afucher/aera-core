using System;
using System.Threading.Tasks;
using aera_core.Domain;
using aera_core.Helpers;
using aera_core.Persistencia;
using AeraIntegrationTest.Builders;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Respawn;

namespace AeraIntegrationTest
{
    public class TurmaRepositórioTeste
    {
        private static string parametrosConexão =
            "Server=localhost;Port=5432;User Id=postgres;Password=root;Database=aera_test";
        private AplicaçãoContexto contexto;
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
            contexto = new AplicaçãoContexto(new DbContextOptionsBuilder()
                .UseNpgsql(parametrosConexão)
                .Options);
            contexto.Database.OpenConnection();
            await _checkpoint.Reset(contexto.Database.GetDbConnection());
        }

        [Test]
        public void DeveRetornarListaDeTurmasVazias()
        {
            var repositório = new TurmaRepositorio(contexto);
            
            var opções = new OpçõesBusca
            {
                LimitePágina = 100,
                Página = 1
            };

            var clientes = repositório.ObterTurmas(opções);

            clientes.Should().BeEmpty();
        }

        [Test]
        public void DeveRetornarTurmasExistentes()
        {
            var turma = new TurmaDBBuilder().Generate(); 
            var entityEntry = contexto.Turmas.Add(turma);
            contexto.SaveChanges();
            entityEntry.State = EntityState.Detached;
            var repositório = new TurmaRepositorio(contexto);
            var opções = new OpçõesBusca
            {
                LimitePágina = 100,
                Página = 1
            };
            var turmas = repositório.ObterTurmas(opções);

            turmas.Should().BeEquivalentTo(turmas, options => options.Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation)).WhenTypeIs<DateTime>());
        }
        
    }
}