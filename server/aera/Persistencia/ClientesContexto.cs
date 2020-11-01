using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace aera_core.Persistencia
{
    public class ClientesContexto : DbContext
    {
        public DbSet<ClienteDB> Clientes { get; set; }
        public DbSet<TurmaDB> Turmas { get; set; }
        public ClientesContexto(DbContextOptions opções) : base(opções) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<ClienteDB>()
            //     .HasMany(p => p.Turmas)
            //     .WithMany(c => c.Alunos)
            //     .UsingEntity<TurmaAluno>(
            //         j => j
            //             .HasOne(ta => ta.Turma)
            //             .WithMany(t => t.TurmaAlunos)
            //             .HasForeignKey(ta => ta.TurmaId),
            //         j => j
            //             .HasOne(ta => ta.Cliente)
            //             .WithMany(c => c.TurmaAlunos)
            //             .HasForeignKey(ta => ta.ClienteId),
            //         j => j.HasKey(t => new {t.TurmaId, t.ClienteId}));
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