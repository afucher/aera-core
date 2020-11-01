using System.Collections.Generic;
using System.Linq;

namespace aera_core.Persistencia
{
    public class TurmaRepositorio
    {
        
        private readonly AplicaçãoContexto _contexto;
        public TurmaRepositorio(AplicaçãoContexto contexto)
        {
            _contexto = contexto;
        }

        public List<TurmaDB> Obter()
        {
            return _contexto.Turmas.Take(2).ToList();
        }
    }
}