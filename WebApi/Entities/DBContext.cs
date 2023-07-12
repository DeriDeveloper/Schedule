﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Entities;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<College> Colleges { get; set; }

    public virtual DbSet<File> Files { get; set; }

    public virtual DbSet<FileMetadatum> FileMetadata { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<StudentDetail> StudentDetails { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAccessToken> UserAccessTokens { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         => optionsBuilder.UseSqlServer(Program.config.GetConnectionString("DBConnectionString"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FileMetadatum>(entity =>
        {
            entity.HasOne(d => d.File).WithMany(p => p.FileMetadata)
                .HasForeignKey(d => d.FileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FileMetadata_Files");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasOne(d => d.College).WithMany(p => p.Groups)
                .HasForeignKey(d => d.CollegeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Groups_Colleges");
        });

        modelBuilder.Entity<StudentDetail>(entity =>
        {
            entity.HasOne(d => d.Group).WithMany(p => p.StudentDetails)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentDetails_Groups");

            entity.HasOne(d => d.User).WithMany(p => p.StudentDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentDetails_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(254);

            entity.HasOne(d => d.AvatarProfileFileMetadatum).WithMany(p => p.Users)
                .HasForeignKey(d => d.AvatarProfileFileMetadatumId)
                .HasConstraintName("FK_Users_FileMetadata");

            entity.HasOne(d => d.UserRole).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_UserRoles");
        });

        modelBuilder.Entity<UserAccessToken>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.Property(e => e.Guid).ValueGeneratedNever();
            entity.Property(e => e.DateCreated).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.UserAccessTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserAccessTokens_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
