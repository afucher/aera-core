using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aera_core.Persistencia
{
    [Table("Courses")]
    public class CursoDB
    {
        
        public CursoDB()
        { 
            createdAt = DateTime.Now; 
            updatedAt = DateTime.Now;
        }
        [Key]
        public int id { get; set; }

        public string name { get; set; }
        public string description { get; set; }
        public int courseLoad { get; set; }

        public List<TurmaDB> Turmas { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}