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
            IQueryable<ClienteDB> consultaClientes = _contexto.Clientes.OrderBy(c => c.name);
            if (opções.Filtros.ContainsKey("search") && opções.Filtros["search"] != null)
            {
                consultaClientes = consultaClientes.Where(c => c.name.ToLower().Contains(opções.Filtros["search"].ToLower()));
            }
            var total = consultaClientes.Count();
            var clientes =  consultaClientes
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
        public ClienteDB Atualizar(ClienteDB clienteParaAtualizar)
        {
            var cliente = _contexto.Clientes.First(c => c.id == clienteParaAtualizar.id);
            cliente.name = clienteParaAtualizar.name;
            cliente.cpf = clienteParaAtualizar.cpf;
            cliente.email = clienteParaAtualizar.email;
            cliente.phone = clienteParaAtualizar.phone;
            cliente.teacher = clienteParaAtualizar.teacher;
            cliente.cel_phone = clienteParaAtualizar.cel_phone;
            cliente.com_phone = clienteParaAtualizar.com_phone;
            cliente.address1 = clienteParaAtualizar.address1;
            cliente.address2 = clienteParaAtualizar.address2;
            cliente.address3 = clienteParaAtualizar.address3;
            cliente.city = clienteParaAtualizar.city;
            cliente.state = clienteParaAtualizar.state;
            cliente.zip_code = clienteParaAtualizar.zip_code;
            cliente.profession = clienteParaAtualizar.profession;
            cliente.edu_lvl = clienteParaAtualizar.edu_lvl;
            cliente.old_code = clienteParaAtualizar.old_code;
            cliente.birth_date = clienteParaAtualizar.birth_date;
            cliente.birth_hour = clienteParaAtualizar.birth_hour;
            cliente.birth_place = clienteParaAtualizar.birth_place;
            cliente.note = clienteParaAtualizar.note;
            _contexto.Clientes.Update(cliente);
            _contexto.SaveChanges();

            return cliente;
        }
    }
}