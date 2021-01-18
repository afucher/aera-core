using System.Collections.Generic;
using aera_core.Helpers;
using aera_core.Models;
using aera_core.Persistencia;

namespace aera_core.Domain
{
    public interface IPagamentosPort
    {
        public ListaPaginada<PagamentoDB> ObterPagamentos(OpçõesBusca opçõesBusca);
        public PagamentoDB Obter(int id);
    }
}