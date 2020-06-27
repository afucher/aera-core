using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aera_core.Persistencia
{
    [Table("Clients")]
    public class ClienteDB
    {
        public ClienteDB()
        { 
            createdAt = DateTime.Now; 
            updatedAt = DateTime.Now;
        }
        
        [Key]
        public int id { get; set; }

        public string name { get; set; }
        public string cpf { get; set; }
        public string email { get; set; }
        public string phone;
        public bool teacher;
        public string cel_phone;
        public string com_phone;
        public string address1;
        public string address2;
        public string address3;
        public string city;
        public string state;
        public string zip_code;
        public string profession;
        public string edu_lvl;
        public string old_code;
        public DateTime birthDate;
        public TimeSpan birthTime;
        public string birthPlace;
        public string note;

        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}