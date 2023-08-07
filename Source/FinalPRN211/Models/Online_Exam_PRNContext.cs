using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FinalPRN211.Models
{
    public partial class Online_Exam_PRNContext : DbContext
    {
        public Online_Exam_PRNContext()
        {
        }

        public Online_Exam_PRNContext(DbContextOptions<Online_Exam_PRNContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; } = null!;
        public virtual DbSet<Grade> Grades { get; set; } = null!;
        public virtual DbSet<History> Histories { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<Test> Tests { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=RYUU\\HUNG;database=Online_Exam_PRN;uid=sa;pwd=123;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("Answer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.IsCorrect).HasColumnName("isCorrect");

                entity.Property(e => e.QuestionId).HasColumnName("questionId");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Answer__question__4222D4EF");
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.ToTable("Grade");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.ToTable("History");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Answer).HasColumnName("answer");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Mark).HasColumnName("mark");

                entity.Property(e => e.Question).HasColumnName("question");

                entity.Property(e => e.TestId).HasColumnName("testId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.Histories)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__History__testId__44FF419A");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Histories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__History__userId__45F365D3");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.IsMore).HasColumnName("isMore");

                entity.Property(e => e.TestId).HasColumnName("testId");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Question__testId__3F466844");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.ToTable("Test");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GradeId).HasColumnName("gradeId");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.GradeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Test__gradeId__3C69FB99");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("userName");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
