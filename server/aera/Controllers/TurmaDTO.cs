using System;
using aera_core.Persistencia;

namespace aera_core.Controllers
{
    public class TurmaDTO
    {
        public int id { get; set; }
        public String Curso { get; set; }
        public int CursoId { get; set; }
        public String DataInicial { get; set; }
        public String DataFinal { get; set; }
        public String HorárioInicial { get; set; }
        public String HorárioFinal { get; set; }
        public int QuantidadeDeAulas { get; set; }
        
        public static TurmaDTO De(TurmaDB turma){
            return new TurmaDTO
            {
                id = turma.id,
                Curso = turma.Curso.name,
                CursoId = turma.Curso.id,
                DataInicial = turma.start_date.ToString("yyyy-MM-dd"),
                DataFinal = turma.end_date.ToString("yyyy-MM-dd"),
                HorárioInicial = turma.start_hour.ToString(@"hh\:mm"),
                HorárioFinal = turma.end_hour.ToString(@"hh\:mm"),
                QuantidadeDeAulas = turma.classes
            };
        }

        public TurmaDB ParaModelo()
        {
            return new TurmaDB
            {
                id = id,
                classes = QuantidadeDeAulas,
                start_date = DateTime.ParseExact(DataInicial, "yyyy-MM-dd",
                    System.Globalization.CultureInfo.InvariantCulture),
                end_date = DateTime.ParseExact(DataFinal, "yyyy-MM-dd",
                    System.Globalization.CultureInfo.InvariantCulture),
                start_hour = TimeSpan.Parse(HorárioInicial),
                end_hour = TimeSpan.Parse(HorárioFinal)
            };
        }
    }
    
    
}