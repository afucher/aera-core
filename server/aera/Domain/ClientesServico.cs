using System.Collections.Generic;
using aera_core.Helpers;

namespace aera_core.Domain
{
    public class ClientesServiço
    {
        private readonly IClientesPort _clientesPort;

        public ClientesServiço(IClientesPort _clientesPort)
        {
            this._clientesPort = _clientesPort;
        }

        public Cliente Obter(int id)
        {
            return _clientesPort.Obter(id);
        }
        public ListaPaginada<Cliente> ObterClientes(OpçõesBusca opções)
        {
            return _clientesPort.ObterClientes(opções);
        }
    }
}