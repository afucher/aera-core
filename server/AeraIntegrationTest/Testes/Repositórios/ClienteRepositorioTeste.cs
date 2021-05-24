using aera_core.Helpers;
using aera_core.Persistencia;
using AeraIntegrationTest.Builders;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Respawn;

namespace AeraIntegrationTest
{
    public class ClienteRepositórioTeste : BaseTesteBanco
    {

        [TearDown]
        public void TearDown()
        {
            _contextoParaTestes.Database.EnsureDeleted();
        }
        [Test]
        public void DeveRetornarListaDeClientesVazias()
        {
            var repositório = new ClienteRepositório(_contextoParaTestes);
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
            var entityEntry = _contextoParaTestes.Clientes.Add(cliente);
            _contextoParaTestes.SaveChanges();
            entityEntry.State = EntityState.Detached;
            var repositório = new ClienteRepositório(_contextoParaTestes);
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
            _contextoParaTestes.Clientes.AddRange(clientesDB);
            _contextoParaTestes.SaveChanges();
            var opções = new OpçõesBusca
            {
                LimitePágina = 5,
                Página = 1
            };
            
            var repositório = new ClienteRepositório(_contextoParaTestes);

            var clientes = repositório.ObterClientes(opções);

            clientes.Should().HaveCount(5);
        }

        [Test]
        public void DeveRetornarClientePeloId()
        {
            var clientesDB = new ClienteDBBuilder().Generate(10); 
            _contextoParaTestes.Clientes.AddRange(clientesDB);
            _contextoParaTestes.SaveChanges();
            
            var repositório = new ClienteRepositório(_contextoParaTestes);

            var cliente = repositório.Obter(clientesDB[2].id);

            cliente.Should().BeEquivalentTo(clientesDB[2].ParaCliente());
        }
        
    }
}