using System;

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
        public DateTime? data_nascimento { get; set; }
        public TimeSpan? hora_nascimento { get; set; }
        public string local_ascimento { get; set; }
        public string observacao { get; set; }
    }
}