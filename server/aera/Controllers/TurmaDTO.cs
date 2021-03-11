using System;
using System.Collections.Generic;
using System.Linq;
using aera_core.Persistencia;

namespace aera_core.Controllers
{
    public class TurmaDTO
    {
        public int Id { get; set; }
        public String Curso { get; set; }
        public int CursoId { get; set; }
        public String DataInicial { get; set; }
        public String DataFinal { get; set; }
        public String HorárioInicial { get; set; }
        public String HorárioFinal { get; set; }
        public int QuantidadeDeAulas { get; set; }
        public int ProfessorId { get; set; }
        public String Professor { get; set; }
        public bool EmAndamento { get; set; }
        public List<TurmaClienteDTO> Alunos { get; set; }

        public class TurmaClienteDTO
        {
            public int id { get; set; }
            public string nome { get; set; }
            public string email { get; set; }
            public string celular { get; set; }
            public int frequencia { get; set; }

        public List<TurmaDTO> turmas {get; set; }
        }

        public static TurmaDTO De(TurmaDB turma){
            return new TurmaDTO
            {
                Id = turma.id,
                Curso = turma.Curso.name,
                CursoId = turma.Curso.id,
                DataInicial = turma.start_date.ToString("yyyy-MM-dd"),
                DataFinal = turma.end_date.ToString("yyyy-MM-dd"),
                HorárioInicial = turma.start_hour.ToString(@"hh\:mm"),
                HorárioFinal = turma.end_hour.ToString(@"hh\:mm"),
                QuantidadeDeAulas = turma.classes,
                Professor = turma.Professor?.name,
                ProfessorId = turma.teacher_id,
                Alunos = turma.TurmaAlunos?.Select(matricula =>
                {
                    return new TurmaClienteDTO
                    {
                        id = matricula.Cliente.id,
                        nome = matricula.Cliente.name,
                        celular = matricula.Cliente.cel_phone,
                        email = matricula.Cliente.email,
                        frequencia = matricula.frequencia
                    };
                }).ToList()
            };
        }

        public TurmaDB ParaModelo()
        {
            return new TurmaDB
            {
                id = Id,
                classes = QuantidadeDeAulas,
                start_date = DateTime.ParseExact(DataInicial, "yyyy-MM-dd",
                    System.Globalization.CultureInfo.InvariantCulture),
                end_date = DateTime.ParseExact(DataFinal, "yyyy-MM-dd",
                    System.Globalization.CultureInfo.InvariantCulture),
                start_hour = TimeSpan.Parse(HorárioInicial),
                end_hour = TimeSpan.Parse(HorárioFinal),
                teacher_id = ProfessorId
            };
        }
    }
    
    
}