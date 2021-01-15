using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace aera_core.Controllers
{
    public class TurmaDTO
    {
        public int id { get; set; }
        public String Curso { get; set; }
        public String DataInicial { get; set; }
        public String DataFinal { get; set; }
    }
}