﻿using System;
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

                entity.HasOne(d => d.Assets)
                    .WithMany(p => p.AssetsAssignedDetails)
                    .HasForeignKey(d => d.AssetsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Assets_Assigned_Details_Assets_Details");

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
            });

            modelBuilder.Entity<EmployeeDetail>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.ToTable("Employee_Details");

                entity.Property(e => e.EmployeeAddress)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.EmployeeDepName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
