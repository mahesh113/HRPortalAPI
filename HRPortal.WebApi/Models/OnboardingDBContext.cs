using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace HRPortal.WebApi.Models
{
    public partial class OnboardingDBContext : DbContext
    {
        public OnboardingDBContext()
        {
        }

        public OnboardingDBContext(DbContextOptions<OnboardingDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DepartmentRef> DepartmentRefs { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<SessionAudit> SessionAudits { get; set; }
        public virtual DbSet<StatusRef> StatusRefs { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=MyDatabase");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<DepartmentRef>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DepartmentRef");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Value)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SessionAudit>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SessionAudit");

                entity.Property(e => e.Isloggedin).HasColumnName("isloggedin");

                entity.Property(e => e.Logintime)
                    .HasColumnType("datetime")
                    .HasColumnName("logintime");
            });

            modelBuilder.Entity<StatusRef>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("StatusRef");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Value)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordSalt)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
