using System;
using System.Collections.Generic;
using System.Linq;
using aera_core.Domain;
using aera_core.Helpers;
using Microsoft.EntityFrameworkCore;

namespace aera_core.Persistencia
{
    public class ClienteRepositório : IClientesPort
    {
        private readonly AplicaçãoContexto _contexto;

        public ClienteRepositório(AplicaçãoContexto contexto)
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
        public ListaPaginada<Cliente> ObterClientes(OpçõesBusca opções)
        {
            var total = _contexto.Clientes.Count();
            var clientes =  _contexto.Clientes
                .Include(c => c.Turmas)
                .Skip((opções.Página-1) * opções.LimitePágina)
                .Take(opções.LimitePágina)
                .Select(x => retornaCliente(x)).ToList();
            return new ListaPaginada<Cliente>(clientes, total, opções.Página, opções.LimitePágina);
        }
    }
}