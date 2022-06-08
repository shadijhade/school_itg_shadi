using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using school_itg_shadi.Models;
using Type = school_itg_shadi.Models.Type;

namespace school_itg_shadi.data
{
    public partial class school_shadiContext : DbContext
    {
        public school_shadiContext()
        {
        }

        public school_shadiContext(DbContextOptions<school_shadiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Association> Associations { get; set; } = null!;
        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Gender> Genders { get; set; } = null!;
        public virtual DbSet<StudentMark> StudentMarks { get; set; } = null!;
        public virtual DbSet<Type> Types { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-QI2L3F5\\SQLEXPRESS;Initial Catalog=school_shadi;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Association>(entity =>
            {
                entity.ToTable("Association");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Associations)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK__Associati__Class__3B75D760");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Associations)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK__Associati__Clien__3A81B327");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.Property(e => e.ClassId)
                    .ValueGeneratedNever()
                    .HasColumnName("Class_ID");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Class_Name");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Client__206D9190849CF402");

                entity.ToTable("Client");

                entity.HasIndex(e => e.UserName, "UQ__Client__681E8A60A8BFB65E")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "UQ__Client__737584F6C27CD9B5")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.GenderId).HasColumnName("Gender_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("Type_ID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("User_Name");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.GenderId)
                    .HasConstraintName("FK__Client__Gender_I__2E1BDC42");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK__Client__Type_ID__2F10007B");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("Gender");

                entity.Property(e => e.GenderId)
                    .ValueGeneratedNever()
                    .HasColumnName("Gender_ID");

                entity.Property(e => e.GenderDesc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Gender_Desc")
                    .HasDefaultValueSql("('Male')");
            });

            modelBuilder.Entity<StudentMark>(entity =>
            {
                entity.ToTable("Student_Marks");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.StudentId).HasColumnName("Student_ID");

                entity.Property(e => e.StudentName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Student_Name");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentMarkStudents)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__Student_M__Stude__37A5467C");

                entity.HasOne(d => d.StudentNameNavigation)
                    .WithMany(p => p.StudentMarkStudentNameNavigations)
                    .HasPrincipalKey(p => p.Name)
                    .HasForeignKey(d => d.StudentName)
                    .HasConstraintName("FK__Student_M__Stude__36B12243");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.Property(e => e.TypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("Type_ID");

                entity.Property(e => e.TypeDesc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Type_Desc")
                    .HasDefaultValueSql("('Student')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
