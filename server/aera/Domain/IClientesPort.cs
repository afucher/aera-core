using System.Collections.Generic;
using aera_core.Helpers;
using aera_core.Persistencia;

namespace aera_core.Domain
{
    public interface IClientesPort
    {
        public ListaPaginada<Cliente> ObterClientes(OpçõesBusca opçõesBusca);
        public Cliente Obter(int id);
        public ClienteDB Criar(ClienteDB cliente);
        public ClienteDB Atualizar(ClienteDB cliente);
    }
}