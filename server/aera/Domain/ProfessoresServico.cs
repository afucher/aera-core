using System.Collections.Generic;
using aera_core.Helpers;

namespace aera_core.Domain
{
    public class ProfessoresServiço
    {
        private readonly IProfessoresPort _professoresPort;

        public ProfessoresServiço(IProfessoresPort _professoresPort)
        {
            this._professoresPort = _professoresPort;
        }
        
        public ListaPaginada<Cliente> ObterTodos(OpçõesBusca opções)
        {
            return _professoresPort.ObterProfessores(opções);
        }

        public Cliente Obter(int id)
        {
            return _professoresPort.ObterProfessor(id);
        }
    }
}