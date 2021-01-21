using System;
using System.Linq;
using aera_core.Domain;
using aera_core.Helpers;
using aera_core.Models;
using Microsoft.EntityFrameworkCore;

namespace aera_core.Persistencia
{
    public class PagamentoRepositório : IPagamentosPort
    {
        
        private readonly AplicaçãoContexto _contexto;
        public PagamentoRepositório(AplicaçãoContexto contexto)
        {
            _contexto = contexto;
        }

        public ListaPaginada<PagamentoDB> ObterPagamentos(OpçõesBusca opções)
        {
            var total = _contexto.Pagamentos.Count();
            var pagamentos =  _contexto.Pagamentos
                .OrderBy(p => p.DueDate)
                .Skip((opções.Página-1) * opções.LimitePágina)
                .Take(opções.LimitePágina)
                .Include(p => p.TurmaAluno)
                .ThenInclude(ta => ta.Cliente)
                .ToList();
            return new ListaPaginada<PagamentoDB>(pagamentos, total, opções.Página, opções.LimitePágina);
        }
        
        public PagamentoDB Obter(int id)
        {
            throw new NotImplementedException();
        }

        public PagamentoDB Pagar(int clientGroupId, int installment)
        {
            var pagamento = _contexto.Pagamentos
                .First(p => p.ClientGroupId == clientGroupId && p.Installment == installment);

            pagamento.Paid = true;
            _contexto.Pagamentos.Update(pagamento);
            _contexto.SaveChanges();

            return pagamento;
        }
    }
}