using System;
using System.Collections.Generic;
using System.Linq;
using aera_core.Helpers;
using aera_core.Models;
using aera_core.Persistencia;

namespace aera_core.Domain
{
    public class PagamentosServiço
    {
        private readonly IPagamentosPort _pagamentosPort;

        public PagamentosServiço(IPagamentosPort pagamentosPort)
        {
            _pagamentosPort = pagamentosPort;
        }

        
        public ListaPaginada<PagamentoDB> ObterPagamentos(OpçõesBusca opções)
        {
            return _pagamentosPort.ObterPagamentos(opções);
        }

        public PagamentoDB Pagar(int clientGroupId, int installment)
        {
            return _pagamentosPort.Pagar(clientGroupId, installment);
        }
        
        public IReadOnlyCollection<PagamentoDB> ObterDoAlunoDaTurma(int turmaId, int alunoId)
        {
            return _pagamentosPort.ObterDoAlunoDaTurma(turmaId, alunoId);
        }

        public void GerarPagamentos(TurmaDB turma, int parcelas, decimal valor, DateTime data)
        {
            var pagamentos =
                turma.TurmaAlunos.SelectMany(ta => geraPagamentosParaMatricula(ta, parcelas, valor, data));
            _pagamentosPort.AdicionarPagamentos(pagamentos.ToList());
        }

        private List<PagamentoDB> geraPagamentosParaMatricula(TurmaAluno matricula ,int parcelas, decimal valor, DateTime data)
        {
            var pagamentosExistentes = _pagamentosPort.ObterDoAlunoDaTurma(matricula.TurmaId, matricula.ClienteId);
            if(pagamentosExistentes.Count > 0) return new List<PagamentoDB>();
           
            var pagamentos = new List<PagamentoDB>();
            var parcela = 1;
            while (parcela <= parcelas)
            {
                pagamentos.Add(new PagamentoDB
                {
                    Installment = parcela,
                    NumberInstallments = parcelas,
                    Value = valor,
                    DueDate = data.AddMonths(parcela - 1),
                    Paid = false,
                    ClientGroupId = matricula.id
                });
                parcela++;
            }

            return pagamentos;
        }
    }
}