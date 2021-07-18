using System.Collections.Generic;
using aera_core;
using aera_core.Models;
using aera_core.Persistencia;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AeraIntegrationTest
{
    public static class ExtensaoAplicacaoContexto
    {

        public static EntityEntry<ClienteDB> GravaProfessor(this AplicaçãoContexto contexto, ClienteDB professor) =>
            GravaCliente(contexto, professor);
 
        public static EntityEntry<ClienteDB> GravaCliente(this AplicaçãoContexto contexto, ClienteDB cliente)
        {
            var clienteCriado = contexto.Clientes.Add(cliente);
            contexto.SaveChanges();
            return clienteCriado;
        }

        public static EntityEntry<PagamentoDB> GravaPagamento(this AplicaçãoContexto contexto, PagamentoDB pagamento)
        {
            var pagamentoCriado = contexto.Pagamentos.Add(pagamento);
            contexto.SaveChanges();
            return pagamentoCriado;
        }
        public static void
            GravaProfessores(this AplicaçãoContexto contexto, IReadOnlyCollection<ClienteDB> professores) =>
            GravaClientes(contexto, professores);
        public static void GravaClientes(this AplicaçãoContexto contexto, IReadOnlyCollection<ClienteDB> clientes)
        {
            contexto.Clientes.AddRange(clientes);
            contexto.SaveChanges();
        }
        
        public static EntityEntry<Usuário> GravaUsuario(this AplicaçãoContexto contexto, Usuário usuario)
        {
            var clienteCriado = contexto.Usuarios.Add(usuario);
            contexto.SaveChanges();
            return clienteCriado;
        }
    }
}