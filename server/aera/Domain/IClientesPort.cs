using System.Collections.Generic;

namespace aera_core.Domain
{
    public interface IClientesPort
    {
        public ListaPaginada<Cliente> ObterClientes(int quantidade, int pagina);
    }
}