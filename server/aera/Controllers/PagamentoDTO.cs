using System;
using aera_core.Models;
using aera_core.Persistencia;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace aera_core.Controllers
{
    public class PagamentoDTO
    {
        public decimal Valor { get; set; }
        public int Parcela { get; set; }
        public int TotalDeParcelas { get; set; }
        public bool Pago { get; set; }
        public String NomeAluno { get; set; }

        public static PagamentoDTO De(PagamentoDB pagamento){
            return new PagamentoDTO
            {
                Valor = pagamento.Value,
                Parcela = pagamento.Installment,
                TotalDeParcelas = pagamento.NumberInstallments,
                Pago = pagamento.Paid.GetValueOrDefault(false),
                NomeAluno = pagamento.TurmaAluno.Cliente.name
            };
        }
    }
    
    
}