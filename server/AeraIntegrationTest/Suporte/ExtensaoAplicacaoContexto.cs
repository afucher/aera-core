using System.Collections.Generic;
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

        public static void
            GravaProfessores(this AplicaçãoContexto contexto, IReadOnlyCollection<ClienteDB> professores) =>
            GravaClientes(contexto, professores);
        public static void GravaClientes(this AplicaçãoContexto contexto, IReadOnlyCollection<ClienteDB> clientes)
        {
            contexto.Clientes.AddRange(clientes);
            contexto.SaveChanges();
        }
    }
}