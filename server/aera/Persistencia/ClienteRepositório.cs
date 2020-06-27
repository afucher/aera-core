using System.Collections.Generic;
using System.Linq;
using aera_core.Domain;

namespace aera_core.Persistencia
{
    public class ClienteRepositório : IClientesPort
    {
        private readonly ClientesContexto _contexto;

        public ClienteRepositório(ClientesContexto contexto)
        {
            _contexto = contexto;
        }

        public IReadOnlyCollection<Cliente> ObterClientes()
        {
            return _contexto.Clientes.Select(x => new Cliente
            {
                Nome = x.name,
                CPF = x.cpf,
                Email = x.email
            }).ToArray();
        }
    }
}