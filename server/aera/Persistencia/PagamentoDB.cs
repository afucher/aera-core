using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace aera_core.Models
{
    [Table("Payments")]
    public partial class PagamentoDB
    {
        public int ClientGroupId { get; set; }
        public decimal Value { get; set; }
        public bool? Paid { get; set; }
        public DateTime? DueDate { get; set; }
        public int Installment { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? NumberInstallments { get; set; }

        public virtual TurmaAluno TurmaAluno { get; set; }
    }
}
