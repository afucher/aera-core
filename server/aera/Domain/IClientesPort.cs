using System.Collections.Generic;
using aera_core.Helpers;

namespace aera_core.Domain
{
    public interface IClientesPort
    {
        public ListaPaginada<Cliente> ObterClientes(OpçõesBusca opçõesBusca);
    }
}