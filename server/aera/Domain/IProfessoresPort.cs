using System.Collections.Generic;
using aera_core.Helpers;
using aera_core.Persistencia;

namespace aera_core.Domain
{
    public interface IProfessoresPort
    {
        public ListaPaginada<Cliente> ObterProfessores(OpçõesBusca opçõesBusca);
        public Cliente ObterProfessor(int id);
    }
}