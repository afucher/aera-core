using System;

namespace aera_core.Controllers
{
    public class TurmaDTO
    {
        public int id { get; set; }
        public String Curso { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
    }
}