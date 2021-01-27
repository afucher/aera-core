using System;
using System.Collections.Generic;
using aera_core.Controllers;
using aera_core.Helpers;
using aera_core.Persistencia;

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

        public ClienteDB Criar(ClienteDB cliente)
        {
            var mesmoCPF = _clientesPort.ObterPorCpf(cliente.cpf);
            if (mesmoCPF != null) throw new Exception($"CPF já cadastrado: {mesmoCPF.id}");
            return _clientesPort.Criar(cliente);
        }

        public ClienteDB Atualizar(ClienteDB cliente)
        {
            return _clientesPort.Atualizar(cliente);
        }
    }
}