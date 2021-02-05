using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aera_core.Persistencia
{
    [Table("Groups")]
    public class TurmaDB
    {
        
        public TurmaDB()
        { 
            createdAt = DateTime.Now; 
            updatedAt = DateTime.Now;
        }
        [Key]
        public int id { get; set; }

        public int course_id { get; set; }
        
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        
        public TimeSpan start_hour { get; set; }
        public TimeSpan end_hour { get; set; }
        public int classes { get; set; }
        
        public ICollection<ClienteDB> Alunos { get; set; }
        public List<TurmaAluno> TurmaAlunos { get; set; }
        public int teacher_id { get; set; }
        public ClienteDB Professor { get; set; }
        public CursoDB Curso { get; set; } 
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}