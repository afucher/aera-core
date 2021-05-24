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
    public class TurmaRepositórioTeste : BaseTesteBanco
    {
        [TearDown]
        public void TearDown()
        {
            _contextoParaTestes.Database.EnsureDeleted();
        }
        
        [Test]
        public void DeveRetornarListaDeTurmasVazias()
        {
            var repositório = new TurmaRepositorio(_contextoParaTestes);
            
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
            var entityEntry = _contextoParaTestes.Turmas.Add(turma);
            _contextoParaTestes.SaveChanges();
            entityEntry.State = EntityState.Detached;
            var repositório = new TurmaRepositorio(_contextoParaTestes);
            var opções = new OpçõesBusca
            {
                LimitePágina = 100,
                Página = 1
            };
            var turmas = repositório.ObterTurmas(opções);

            turmas.Should().BeEquivalentTo(new [] {turma}, options => options.Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation)).WhenTypeIs<DateTime>());
        }
        
    }
}