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
        public String Descricao { get; set; }
        public int CargaHoraria { get; set; }

        public static CursoDTO De(CursoDB curso){
            return new CursoDTO
            {
                id = curso.id,
                Nome = curso.name,
                Descricao = curso.description,
                CargaHoraria = curso.courseLoad
            };
        }

        public CursoDB ParaModelo()
        {
            return new CursoDB
            {
                id = id,
                name = Nome,
                description = Descricao,
                courseLoad = CargaHoraria
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