using System.Collections.Generic;

namespace aera_core.Helpers
{
    public class OpçõesBusca
    {
        public int Página { get; set; }
        public int LimitePágina { get; set; }

        public Dictionary<string, object> Filtros; 
    }
}