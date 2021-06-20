using aera_core.Persistencia;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AeraIntegrationTest
{
    public static class ExtensaoAplicacaoContexto
    {
        public static EntityEntry<ClienteDB> AdicionaESalva(this AplicaçãoContexto contexto, ClienteDB professor)
        {
            var professorCriado = contexto.Clientes.Add(professor);
            contexto.SaveChanges();
            return professorCriado;
        }
    }
}