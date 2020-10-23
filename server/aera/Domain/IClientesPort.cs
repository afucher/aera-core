using System.Collections.Generic;

namespace aera_core.Domain
{
    public interface IClientesPort
    {
        public IReadOnlyCollection<Cliente> ObterClientes(int quantidade);
    }
}