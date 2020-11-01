using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace aera_core.Persistencia
{
    public class AplicaçãoContexto : DbContext
    {
        public DbSet<ClienteDB> Clientes { get; set; }
        public DbSet<TurmaDB> Turmas { get; set; }
        public AplicaçãoContexto(DbContextOptions opções) : base(opções) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
        }

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
}