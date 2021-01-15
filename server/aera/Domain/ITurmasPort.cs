using System.Collections.Generic;
using aera_core.Helpers;
using aera_core.Persistencia;

namespace aera_core.Domain
{
    public interface ITurmasPort
    {
        public ListaPaginada<TurmaDB> ObterTurmas(OpçõesBusca opçõesBusca);
        public TurmaDB Obter(int id);
    }
}