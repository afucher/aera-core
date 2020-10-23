using System.Collections.Generic;

namespace aera_core.Domain
{
    public class ClientesServiço
    {
        private readonly IClientesPort _clientesPort;

        public ClientesServiço(IClientesPort _clientesPort)
        {
            this._clientesPort = _clientesPort;
        }
        public IReadOnlyCollection<Cliente> ObterClientes(int quantidade, int pagina)
        {
            return _clientesPort.ObterClientes(quantidade, pagina);
        }
    }
}