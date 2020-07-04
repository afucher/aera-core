using System;

namespace aera_core.Domain
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public bool ÉProfessor { get; set; }
        public string Celular { get; set; }
        public string TelefoneComercial { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
        public string Profissão { get; set; }
        public string NívelEducação { get; set; }
        public string CódigoAntigo { get; set; }
        public DateTime? DataNascimento { get; set; }
        public TimeSpan? HorárioNascimento { get; set; }
        public string LocalNascimento { get; set; }
        public string Observação { get; set; }
    }
}