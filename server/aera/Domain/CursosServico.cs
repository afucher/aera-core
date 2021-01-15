using System.Collections.Generic;
using aera_core.Helpers;
using aera_core.Persistencia;

namespace aera_core.Domain
{
    public class CursosServiço
    {
        private readonly ICursosPort _cursosPort;

        public CursosServiço(ICursosPort cursosPort)
        {
            _cursosPort = cursosPort;
        }

        public CursoDB Obter(int id)
        {
            return _cursosPort.Obter(id);
        }
        public ListaPaginada<CursoDB> ObterCursos(OpçõesBusca opções)
        {
            return _cursosPort.ObterCursos(opções);
        }
    }
}