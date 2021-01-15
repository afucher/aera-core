using System.Collections.Generic;
using System.Linq;
using aera_core.Domain;
using aera_core.Helpers;
using Microsoft.EntityFrameworkCore;

namespace aera_core.Persistencia
{
    public class TurmaRepositorio : ITurmasPort
    {
        
        private readonly AplicaçãoContexto _contexto;
        public TurmaRepositorio(AplicaçãoContexto contexto)
        {
            _contexto = contexto;
        }

        public ListaPaginada<TurmaDB> ObterTurmas(OpçõesBusca opções)
        {
            var total = _contexto.Turmas.Count();
            var turmas =  _contexto.Turmas
                .Include(t => t.Curso)
                .Skip((opções.Página-1) * opções.LimitePágina)
                .Take(opções.LimitePágina)
                .ToList();
            return new ListaPaginada<TurmaDB>(turmas, total, opções.Página, opções.LimitePágina);
        }

        public TurmaDB Obter(int id)
        {
            return _contexto.Turmas
                .Include(t => t.Curso)
                .FirstOrDefault(t => t.id == id);
        }
    }
}