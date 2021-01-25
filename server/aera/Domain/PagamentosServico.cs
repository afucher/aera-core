using System.Collections.Generic;
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
    }
}