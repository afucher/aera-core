using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aera_core.Persistencia
{
    [Table("Courses")]
    public class TurmaDB
    {
        
        public TurmaDB()
        { 
            createdAt = DateTime.Now; 
            updatedAt = DateTime.Now;
        }
        [Key]
        public int id { get; set; }

        public string name { get; set; }
        
        public ICollection<ClienteDB> Alunos { get; set; }
        public List<TurmaAluno> TurmaAlunos { get; set; }
        
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}