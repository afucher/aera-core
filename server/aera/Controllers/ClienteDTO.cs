using System;
using System.Collections.Generic;
using aera_core.Domain;
using aera_core.Persistencia;

namespace aera_core.Controllers
{
    public class ClienteDTO
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
        public bool professor { get; set; }
        public string celular { get; set; }
        public string telefone_comercial { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string cep { get; set; }
        public string profissao { get; set; }
        public string nivel_educacao { get; set; }
        public String data_nascimento { get; set; }
        public String hora_nascimento { get; set; }
        public string local_nascimento { get; set; }
        public string observacao { get; set; }
        
        public List<TurmaDTO> turmas { get; set; }

        public Cliente ParaModelo()
        {
            return new Cliente
            {
                Id = id,
                Nome = nome,
                CPF = cpf,
                Email = email,
                Telefone = telefone,
                ÉProfessor = professor,
                Celular = celular,
                TelefoneComercial = telefone_comercial,
                address1 = address1,
                address2 = address2,
                address3 = address3,
                Cidade = cidade,
                Estado = estado,
                CEP = cep,
                Profissão = profissao,
                NívelEducação = nivel_educacao,
                DataNascimento = data_nascimento == null ? (DateTime?)null : DateTime.ParseExact(data_nascimento, "yyyy-MM-dd",
                    System.Globalization.CultureInfo.InvariantCulture),
                HorárioNascimento = hora_nascimento == null ? (TimeSpan?)null : TimeSpan.Parse(hora_nascimento),
                LocalNascimento = local_nascimento,
                Observação = observacao
            };
        }
    }
}