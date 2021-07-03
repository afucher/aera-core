using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using aera_core.Models;
using aera_core.Persistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace aera_core.Persistencia
{
    public class AplicaçãoContexto : DbContext
    {
        public DbSet<ClienteDB> Clientes { get; set; }
        public DbSet<TurmaDB> Turmas { get; set; }
        public DbSet<CursoDB> Cursos { get; set; }
        public DbSet<PagamentoDB> Pagamentos { get; set; }
        public DbSet<TurmaAluno> Matriculas { get; set; }
        
        public DbSet<User> Usuarios { get; set; }
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

            modelBuilder.Entity<TurmaDB>()
                .HasOne(t => t.Curso)
                .WithMany(t => t.Turmas)
                .HasForeignKey(x => x.course_id);

            modelBuilder.Entity<TurmaDB>()
                .HasOne(t => t.Professor)
                .WithMany()
                .HasForeignKey(t => t.teacher_id);

            modelBuilder.Entity<PagamentoDB>(entity =>
            {
                entity.HasKey(e => new { e.ClientGroupId, e.Installment })
                    .HasName("Payments_pkey");

                entity.Property(e => e.ClientGroupId).HasColumnName("clientGroup_id");

                entity.Property(e => e.Installment)
                    .HasColumnName("installment")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("createdAt");

                entity.Property(e => e.DueDate)
                    .HasColumnType("date")
                    .HasColumnName("due_date");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.NumberInstallments).HasColumnName("number_installments");

                entity.Property(e => e.Paid)
                    .HasColumnName("paid")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("updatedAt");

                entity.Property(e => e.Value)
                    .HasPrecision(10, 2)
                    .HasColumnName("value");

                entity.HasOne(d => d.TurmaAluno)
                    .WithMany(p => p.Pagamentos)
                    .HasForeignKey(d => d.ClientGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkey_group_client");
            });
            
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("createdAt");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("updatedAt");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("username");
            });

        }
    }
}

[Table("ClientGroups")]
public class TurmaAluno
{
    
    public TurmaAluno()
    { 
        createdAt = DateTime.Now; 
        updatedAt = DateTime.Now;
    }
    [Key]
    public int id { get; set; }
    
    [Column("client_id")]
    public int ClienteId { get; set; }
    
    public ClienteDB Cliente { get; set; }
    
    [Column("group_id")]
    public int TurmaId { get; set; }
    
    public TurmaDB Turma { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    
    [Column("attendance")]
    public int frequencia { get; set; }
    public virtual ICollection<PagamentoDB> Pagamentos { get; set; }
}
