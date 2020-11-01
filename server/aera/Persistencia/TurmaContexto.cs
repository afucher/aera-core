using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace aera_core.Persistencia
{
    public class TurmaContexto : DbContext
    {
        public DbSet<TurmaDB> Turmas { get; set; }
        public TurmaContexto(DbContextOptions opções) : base(opções) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TurmaDB>()
                .HasMany(p => p.Alunos)
                .WithMany(c => c.Turmas)
                .UsingEntity<TurmaAluno>(
                    j => j
                        .HasOne(ta => ta.Cliente)
                        .WithMany(c => c.TurmaAlunos)
                        .HasForeignKey(ta => ta.ClienteId),
                    j => j
                        .HasOne(ta => ta.Turma)
                        .WithMany(t => t.TurmaAlunos)
                        .HasForeignKey(ta => ta.TurmaId));

        }
    }

    [Table("ClientGroups")]
    public class TurmaAluno
    {
        [Key]
        public int id { get; set; }
        
        [Column("client_id")]
        public int ClienteId { get; set; }
        
        public ClienteDB Cliente { get; set; }
        
        [Column("group_id")]
        public int TurmaId { get; set; }
        
        public TurmaDB Turma { get; set; }
    }
}