using System;
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

    public virtual DbSet<Audience> Audiences { get; set; }

    public virtual DbSet<College> Colleges { get; set; }

    public virtual DbSet<File> Files { get; set; }

    public virtual DbSet<FileMetadatum> FileMetadata { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<HeadOfScheduleDepartmentDetail> HeadOfScheduleDepartmentDetails { get; set; }

    public virtual DbSet<ScheduleCell> ScheduleCells { get; set; }

    public virtual DbSet<ScheduleCellAudience> ScheduleCellAudiences { get; set; }

    public virtual DbSet<ScheduleCellGroup> ScheduleCellGroups { get; set; }

    public virtual DbSet<ScheduleCellTeacher> ScheduleCellTeachers { get; set; }

    public virtual DbSet<ScheduleTypeWeek> ScheduleTypeWeeks { get; set; }

    public virtual DbSet<StudentDetail> StudentDetails { get; set; }

    public virtual DbSet<TeacherDetail> TeacherDetails { get; set; }

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

        modelBuilder.Entity<HeadOfScheduleDepartmentDetail>(entity =>
        {
            entity.HasOne(d => d.College).WithMany(p => p.HeadOfScheduleDepartmentDetails)
                .HasForeignKey(d => d.CollegeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HeadOfScheduleDepartmentDetails_Colleges");

            entity.HasOne(d => d.User).WithMany(p => p.HeadOfScheduleDepartmentDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HeadOfScheduleDepartmentDetails_Users");
        });

        modelBuilder.Entity<ScheduleCell>(entity =>
        {
            entity.Property(e => e.Date).HasColumnType("date");

            entity.HasOne(d => d.TypeWeek).WithMany(p => p.ScheduleCells)
                .HasForeignKey(d => d.TypeWeekId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ScheduleCells_ScheduleTypeWeek");
        });

        modelBuilder.Entity<ScheduleCellAudience>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ScheduleCellAudience");

            entity.HasOne(d => d.Audience).WithMany()
                .HasForeignKey(d => d.AudienceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ScheduleCellAudience_Audiences");

            entity.HasOne(d => d.ScheduleCell).WithMany()
                .HasForeignKey(d => d.ScheduleCellId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ScheduleCellAudience_ScheduleCells");
        });

        modelBuilder.Entity<ScheduleCellGroup>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ScheduleCellGroup");

            entity.HasOne(d => d.Group).WithMany()
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ScheduleCellGroup_Groups");

            entity.HasOne(d => d.ScheduleCell).WithMany()
                .HasForeignKey(d => d.ScheduleCellId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ScheduleCellGroup_ScheduleCells");
        });

        modelBuilder.Entity<ScheduleCellTeacher>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ScheduleCellTeacher");

            entity.HasOne(d => d.ScheduleCell).WithMany()
                .HasForeignKey(d => d.ScheduleCellId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ScheduleCellTeacher_ScheduleCells");

            entity.HasOne(d => d.Teacher).WithMany()
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ScheduleCellTeacher_Users");
        });

        modelBuilder.Entity<ScheduleTypeWeek>(entity =>
        {
            entity.ToTable("ScheduleTypeWeek");
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

        modelBuilder.Entity<TeacherDetail>(entity =>
        {
            entity.HasOne(d => d.CuratorOfGroup).WithMany(p => p.TeacherDetails)
                .HasForeignKey(d => d.CuratorOfGroupId)
                .HasConstraintName("FK_TeacherDetails_Groups");

            entity.HasOne(d => d.User).WithMany(p => p.TeacherDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeacherDetails_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(254);

            entity.HasOne(d => d.AvatarProfileFileMetadatum).WithMany(p => p.Users)
                .HasForeignKey(d => d.AvatarProfileFileMetadatumId)
                .HasConstraintName("FK_Users_FileMetadata");

            entity.HasOne(d => d.College).WithMany(p => p.Users)
                .HasForeignKey(d => d.CollegeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Colleges");

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
