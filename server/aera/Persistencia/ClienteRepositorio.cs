using System;
using System.Collections.Generic;
using System.Linq;
using aera_core.Domain;
using aera_core.Helpers;
using Microsoft.EntityFrameworkCore;

namespace aera_core.Persistencia
{
    public class ClienteRepositório : IClientesPort, IProfessoresPort
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
                Observação = x.note,
                Turmas = x.Turmas?.ToList()
            };
            return cliente;
        }
        public ListaPaginada<Cliente> ObterClientes(OpçõesBusca opções)
        {
            var total = _contexto.Clientes.Count();
            var clientes =  _contexto.Clientes
                .Include(c => c.Turmas)
                .ThenInclude(t => t.Curso)
                .Skip((opções.Página-1) * opções.LimitePágina)
                .Take(opções.LimitePágina)
                .Select(x => retornaCliente(x)).ToList();
            return new ListaPaginada<Cliente>(clientes, total, opções.Página, opções.LimitePágina);
        }
        
        public ListaPaginada<Cliente> ObterProfessores(OpçõesBusca opções)
        {
            var total = _contexto.Clientes
                .OrderBy(c => c.id)
                .Count(c => c.teacher);
            var clientes =  _contexto.Clientes
                .OrderBy(c => c.id)
                .Where(c => c.teacher)
                .Skip((opções.Página-1) * opções.LimitePágina)
                .Take(opções.LimitePágina)
                .Select(x => retornaCliente(x)).ToList();
            return new ListaPaginada<Cliente>(clientes, total, opções.Página, opções.LimitePágina);
        }
        
        public Cliente ObterProfessor(int id)
        { 
            var professor = _contexto.Clientes
                .FirstOrDefault(c => c.teacher && c.id == id);

            return retornaCliente(professor);
        }

        public Cliente Obter(int id)
        {
            return _contexto.Clientes
                .Include(c => c.Turmas)
                .ThenInclude(t => t.Curso)
                .FirstOrDefault(c => c.id == id)?
                .ParaCliente();
        }

        public ClienteDB Criar(ClienteDB cliente)
        {
            var clienteCriado = _contexto.Clientes.Add(cliente);
            _contexto.SaveChanges();

            return clienteCriado.Entity;
        }
    }
}