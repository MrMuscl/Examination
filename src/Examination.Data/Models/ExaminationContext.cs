using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

#nullable disable

namespace Examination.Data.Models
{
    public partial class ExaminationContext : DbContext
    {
        const string DATABASE_NAME = "ExaminationCodeFirst";
        static string _connectionString = $"data source=ANTONK-573;Initial Catalog={DATABASE_NAME};Integrated Security=True;";
        public ExaminationContext()
        {
        }

        public ExaminationContext(DbContextOptions<ExaminationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<Protocol> Protocols { get; set; }
        public virtual DbSet<Attestation> Attestations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString)
                    .LogTo(Console.WriteLine,
                        new[] { DbLoggerCategory.Database.Command.Name,
                                DbLoggerCategory.Database.Transaction.Name},
                        LogLevel.Debug).EnableSensitiveDataLogging();

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("Answers");

                entity.Property(e => e.Id)
                    .HasColumnName("Id");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionId");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Answer_Question");
            });

            modelBuilder.Entity<Protocol>(entity =>
            {
                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.Protocols)
                    .HasForeignKey(d => d.AnswerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Protocols_Answers");

                entity.HasOne(d => d.Attestation)
                    .WithMany(p => p.Protocols)
                    .HasForeignKey(d => d.AttestationId)
                    .HasConstraintName("FK_Protocol_Attestation");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Protocols)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Protocols_Questions");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.Protocols)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Protocols_Tests");
            });

            modelBuilder.Entity<Attestation>(entity =>
            {
                entity.ToTable("Attestations");
            });

            //modelBuilder.Entity<Question>(entity =>
            //{
            //    entity.ToTable("Question");

            //    entity.Property(e => e.Id)
            //        .HasColumnName("ID");

            //    entity.Property(e => e.Text).HasColumnType("text");
            //});

            
            //modelBuilder.Entity<Test>(entity =>
            //{
            //    entity.ToTable("Test");

            //    entity.Property(e => e.Id)
            //        .HasColumnName("ID");

            //    entity.Property(e => e.Name)
            //        .HasMaxLength(255)
            //        .IsFixedLength(true);
            //});

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
