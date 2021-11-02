using System;
using System.Collections.Generic;
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

        public ListaPaginada<PagamentoDB> ObterPagamentosPendentes(OpçõesBusca opções)
        {
            var total = _contexto.Pagamentos.Count();
            var pagamentos =  _contexto.Pagamentos
                .Where(p => p.Paid == false || p.Paid == null)
                .OrderBy(p => p.DueDate)
                .Skip((opções.Página-1) * opções.LimitePágina)
                .Take(opções.LimitePágina)
                .Include(p => p.TurmaAluno)
                .ThenInclude(ta => ta.Cliente)
                .ToList();
            return new ListaPaginada<PagamentoDB>(pagamentos, total, opções.Página, opções.LimitePágina);
        }

        public IReadOnlyCollection<PagamentoDB> ObterPorMatricula(int matriculaId)
        {
            return _contexto.Pagamentos
                .Where(p => p.ClientGroupId == matriculaId).ToList();
        }

        public void AdicionarPagamentos(List<PagamentoDB> pagamentos)
        {
            _contexto.Pagamentos.AddRange(pagamentos);
            _contexto.SaveChanges();
        }

        public IReadOnlyCollection<PagamentoDB> ObterDoAlunoDaTurma(int turmaId, int alunoId)
        {
            return  _contexto.Pagamentos
                .OrderBy(p => p.DueDate)
                .Where(p => p.TurmaAluno.TurmaId == turmaId && p.TurmaAluno.ClienteId == alunoId)
                .ToList();
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