using System.Collections.Generic;
using System.Linq;
using aera_core.Domain;
using aera_core.Helpers;
using Microsoft.EntityFrameworkCore;

namespace aera_core.Persistencia
{
    public class CursoRepositório : ICursosPort
    {
        
        private readonly AplicaçãoContexto _contexto;
        public CursoRepositório(AplicaçãoContexto contexto)
        {
            _contexto = contexto;
        }

        public ListaPaginada<CursoDB> ObterCursos(OpçõesBusca opções)
        {
            var total = _contexto.Cursos.Count();
            var cursos =  _contexto.Cursos
                .Skip((opções.Página-1) * opções.LimitePágina)
                .Take(opções.LimitePágina)
                .ToList();
            return new ListaPaginada<CursoDB>(cursos, total, opções.Página, opções.LimitePágina);
        }
        
        public CursoDB Obter(int id)
        {
            return _contexto.Cursos
                .FirstOrDefault(c => c.id == id);
        }
    }
}