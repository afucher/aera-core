using System;
using aera_core.Persistencia;
using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace aera_core.Controllers
{
    public class CursoDTO
    {
        public int id { get; set; }
        public String Nome { get; set; }
        public String Descrição { get; set; }
        public int CargaHorária { get; set; }

        public static CursoDTO De(CursoDB curso){
            return new CursoDTO
            {
                id = curso.id,
                Nome = curso.name,
                Descrição = curso.description,
                CargaHorária = curso.courseLoad
            };
        }

        public CursoDB ParaModelo()
        {
            return new CursoDB
            {
                id = id,
                name = Nome,
                description = Descrição,
                courseLoad = CargaHorária
            };
        }
    }

    public class CursoDTOValidator : AbstractValidator<CursoDTO>
    {
        public CursoDTOValidator()
        {
            RuleFor(c => c.Nome).NotEmpty();
        } 
    }
    
    
}