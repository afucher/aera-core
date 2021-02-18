using System.Collections.Generic;
using aera_core.Helpers;
using aera_core.Persistencia;

namespace aera_core.Domain
{
    public interface ICursosPort
    {
        public ListaPaginada<CursoDB> ObterCursos(OpçõesBusca opçõesBusca);
        public CursoDB Obter(int id);
        public CursoDB Criar(CursoDB curso);
        public CursoDB Atualizar(CursoDB curso);
    }
}