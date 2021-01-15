using System.Collections.Generic;
using aera_core.Helpers;
using aera_core.Persistencia;

namespace aera_core.Domain
{
    public class TurmasServiço
    {
        private readonly ITurmasPort _turmasPort;

        public TurmasServiço(ITurmasPort _turmasPort)
        {
            this._turmasPort = _turmasPort;
        }

        public TurmaDB Obter(int id)
        {
            return _turmasPort.Obter(id);
        }

        public TurmaDB Criar(TurmaDB turma)
        {
            return _turmasPort.Criar(turma);
        }

        public TurmaDB Atualizar(TurmaDB turma)
        {
            return _turmasPort.Atualizar(turma);
        }
        public ListaPaginada<TurmaDB> ObterTurmas(OpçõesBusca opções)
        {
            return _turmasPort.ObterTurmas(opções);
        }
    }
}