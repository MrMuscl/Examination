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
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Answer_Question");
            });

            modelBuilder.Entity<Attestation>(entity =>
            {
                entity.ToTable("Attestations");
            });
                        
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
