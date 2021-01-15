using System;
using aera_core.Persistencia;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace aera_core.Controllers
{
    public class CursoDTO
    {
        public int id { get; set; }
        public String Nome { get; set; }

        public static CursoDTO De(CursoDB curso){
            return new CursoDTO
            {
                id = curso.id,
                Nome = curso.name
            };
        }
    }
    
    
}