using System;
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



        private static Cliente retornaCliente(ClienteDB x)
        {
            var cliente = new Cliente
            {
                Id = x.id,
                Nome = x.name,
                CPF = x.cpf,
                Email = x.email,
                Telefone = x.phone,
                ÉProfessor = x.teacher,
                Celular = x.cel_phone,
                TelefoneComercial = x.com_phone,
                address1 = x.address1,
                address2 = x.address2,
                address3 = x.address3,
                Cidade = x.city,
                Estado = x.state,
                CEP = x.zip_code,
                Profissão = x.profession,
                NívelEducação = x.edu_lvl,
                CódigoAntigo = x.old_code,
                DataNascimento = x.birth_date,
                HorárioNascimento = x.birth_hour,
                LocalNascimento = x.birth_place,
                Observação = x.note

            };
            return cliente;
        }
        public IReadOnlyCollection<Cliente> ObterClientes(int quantidade, int pagina)
        {
            return _contexto.Clientes.Skip(pagina * quantidade).Take(quantidade).Select(x => retornaCliente(x)).ToArray();
        }
    }
}