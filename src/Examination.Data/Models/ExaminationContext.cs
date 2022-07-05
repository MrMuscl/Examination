using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

#nullable disable

namespace Examination.Data.Models
{
    public partial class ExaminationContext : DbContext
    {
        private IOptions<ExaminationConnectionStrings> _options;

        public ExaminationContext()
        {
        }

        public ExaminationContext(DbContextOptions<ExaminationContext> options)
            : base(options)
        {
        }

        public ExaminationContext(IOptions<ExaminationConnectionStrings> options) 
        {
            _options = options;
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
                optionsBuilder.UseSqlServer(_options.Value.ExaminationDbConnectionString)
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
                entity.Property(e => e.Text).HasMaxLength(2000);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.Text).HasMaxLength(2000);

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.TestId);
            });

            modelBuilder.Entity<Protocol>(entity =>
            {
                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.Protocols)
                    .HasForeignKey(d => d.AnswerId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(d => d.Attestation)
                    .WithMany(p => p.Protocols)
                    .HasForeignKey(d => d.AttestationId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Protocols)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
