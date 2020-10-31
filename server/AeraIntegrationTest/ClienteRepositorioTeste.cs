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
            var opções = new OpçõesBusca
            {
                LimitePágina = 100,
                Página = 1
            };

            var clientes = repositório.ObterClientes(opções);

            clientes.Should().BeEmpty();
        }

        [Test]
        public void DeveRetornarClientesJáExistentes()
        {
            var cliente = new ClienteDBBuilder().Generate(); 
            var entityEntry = contexto.Clientes.Add(cliente);
            contexto.SaveChanges();
            entityEntry.State = EntityState.Detached;
            var repositório = new ClienteRepositório(contexto);
            var opções = new OpçõesBusca
            {
                LimitePágina = 100,
                Página = 1
            };
            
            var clientes = repositório.ObterClientes(opções);

            clientes.Should().BeEquivalentTo(cliente.ParaCliente());
        }
        
        [Test]
        public void DeveRetornarClientesRespeitandoQuantidade()
        {
            var clientesDB = new ClienteDBBuilder().Generate(10); 
            contexto.Clientes.AddRange(clientesDB);
            contexto.SaveChanges();
            var opções = new OpçõesBusca
            {
                LimitePágina = 5,
                Página = 1
            };
            
            var repositório = new ClienteRepositório(contexto);

            var clientes = repositório.ObterClientes(opções);

            clientes.Should().HaveCount(5);
        }
        
    }
}