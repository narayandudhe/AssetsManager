using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AssetsManager.Models
{
    public partial class AssetManagerDBContext : DbContext
    {
        public AssetManagerDBContext()
        {
        }

        public AssetManagerDBContext(DbContextOptions<AssetManagerDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AssetsAssignedDetail> AssetsAssignedDetails { get; set; }
        public virtual DbSet<AssetsDetail> AssetsDetails { get; set; }
        public virtual DbSet<EmployeeDetail> EmployeeDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=AssetManagerDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssetsAssignedDetail>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.ToTable("Assets_Assigned_Details");

                entity.Property(e => e.AssetModel)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.AssetsCompanyName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AssetsSerialNo)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.AsstsName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfAssigned)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.AssetsAssignedDetails)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Assets_Assigned_Details_Employee_Details");
            });

            modelBuilder.Entity<AssetsDetail>(entity =>
            {
                entity.HasKey(e => e.AssetsId);

                entity.ToTable("Assets_Details");

                entity.Property(e => e.AssetImageUrl).HasMaxLength(200);

                entity.Property(e => e.AssetModel)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.AssetPrice).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.AssetPurchaseDate).HasColumnType("date");

                entity.Property(e => e.AssetsCompanyName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AssetsSerialNo)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.AsstsName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmployeeDetail>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.ToTable("Employee_Details");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.DateOfJoining).HasColumnType("date");

                entity.Property(e => e.EmployeeAddress)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.EmployeeDepName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EmployeeEmailId).HasMaxLength(150);

                entity.Property(e => e.EmployeeMobileNo).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeProfilePicUrl)
                    .HasMaxLength(500)
                    .HasColumnName("EmployeeProfilePicURL");

                entity.Property(e => e.EmployeeSalary).HasColumnType("numeric(7, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
