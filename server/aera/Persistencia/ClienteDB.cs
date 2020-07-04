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
        public string phone{ get; set; }
        public bool teacher{ get; set; }
        public string cel_phone{ get; set; }
        public string com_phone{ get; set; }
        public string address1{ get; set; }
        public string address2{ get; set; }
        public string address3{ get; set; }
        public string city{ get; set; }
        public string state{ get; set; }
        public string zip_code{ get; set; }
        public string profession{ get; set; }
        public string edu_lvl{ get; set; }
        public string old_code{ get; set; }
        public DateTime? birth_date{ get; set; }
        public TimeSpan? birth_hour{ get; set; }
        public string birth_place{ get; set; }
        public string note{ get; set; }

        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}