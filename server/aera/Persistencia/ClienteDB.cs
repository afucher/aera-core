using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using aera_core.Domain;

namespace aera_core.Persistencia
{
    [Table("Clients")]
    public class ClienteDB
    {
        public ClienteDB()
        { 
            createdAt = DateTime.Now; 
            updatedAt = DateTime.Now;
        }
        
        [Key]
        public int id { get; set; }

        public string name { get; set; }
        public string cpf { get; set; }
        public string email { get; set; }
        public string phone{ get; set; }
        public bool teacher{ get; set; }
        public string cel_phone{ get; set; }
        public string com_phone{ get; set; }
        public string address1{ get; set; }
        public string address2{ get; set; }
        public string address3{ get; set; }
        public string city{ get; set; }
        public string state{ get; set; }
        public string zip_code{ get; set; }
        public string profession{ get; set; }
        public string edu_lvl{ get; set; }
        public string old_code{ get; set; }
        public DateTime? birth_date{ get; set; }
        public TimeSpan? birth_hour{ get; set; }
        public string birth_place{ get; set; }
        public string note{ get; set; }

        public ICollection<TurmaDB> Turmas { get; set; }
        
        public List<TurmaAluno> TurmaAlunos { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }


        public Cliente ParaCliente()
        {
            System.Console.WriteLine(">>>>>>>>>>>>>>>>>");
            Console.WriteLine(Turmas.Count);
            return new Cliente
            {
                Id = id,
                Nome = name,
                CPF = cpf,
                Email = email,
                Telefone = phone,
                ÉProfessor = teacher,
                Celular = cel_phone,
                TelefoneComercial = com_phone,
                address1 = address1,
                address2 = address2,
                address3 = address3,
                Cidade = city,
                Estado = state,
                CEP = zip_code,
                Profissão = profession,
                NívelEducação = edu_lvl,
                CódigoAntigo = old_code,
                DataNascimento = birth_date,
                HorárioNascimento = birth_hour,
                LocalNascimento = birth_place,
                Observação = note
            };
        }

        public static ClienteDB DeCliente(Cliente cliente)
        {
            return new ClienteDB
            {
                id = cliente.Id,
                name = cliente.Nome,
                cpf = cliente.CPF,
                email = cliente.Email,
                phone = cliente.Telefone,
                teacher = cliente.ÉProfessor,
                cel_phone = cliente.Celular,
                com_phone = cliente.TelefoneComercial,
                address1 = cliente.address1,
                address2 = cliente.address2,
                address3 = cliente.address3,
                city = cliente.Cidade,
                state = cliente.Estado,
                zip_code = cliente.CEP,
                profession = cliente.Profissão,
                edu_lvl = cliente.NívelEducação,
                old_code = cliente.CódigoAntigo,
                birth_date = cliente.DataNascimento,
                birth_hour = cliente.HorárioNascimento,
                birth_place = cliente.LocalNascimento,
                note = cliente.Observação
            };
        }
    }
}