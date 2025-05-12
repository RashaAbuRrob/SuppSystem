using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMvcFull.Models;

public partial class SuppDatabaseContext : DbContext
{
    public SuppDatabaseContext()
    {
    }

    public SuppDatabaseContext(DbContextOptions<SuppDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AllSchool> AllSchools { get; set; }

    public virtual DbSet<AllSchoolsBackup> AllSchoolsBackups { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Datashow> Datashows { get; set; }

    public virtual DbSet<Desktop> Desktops { get; set; }

    public virtual DbSet<Directorate> Directorates { get; set; }

    public virtual DbSet<DirectorateBackup> DirectorateBackups { get; set; }

    public virtual DbSet<InteractiveBoard> InteractiveBoards { get; set; }

    public virtual DbSet<Lab> Labs { get; set; }

    public virtual DbSet<Laptop> Laptops { get; set; }

    public virtual DbSet<Modell> Modells { get; set; }

    public virtual DbSet<NewAllDatum> NewAllData { get; set; }

    public virtual DbSet<Printer> Printers { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<School> Schools { get; set; }

    public virtual DbSet<SupplementsTable> SupplementsTables { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-DICKHML\\SQLEXPRESS;Database=SuppDatabase;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Arabic_CI_AS");

        modelBuilder.Entity<AllSchool>(entity =>
        {
            entity.HasKey(e => e.NationalId).HasName("PK_AllSchools_1");

            entity.Property(e => e.NationalId)
                .ValueGeneratedNever()
                .HasColumnName("NationalID");
            entity.Property(e => e.BteclabsCount).HasColumnName("BTECLabsCount");
            entity.Property(e => e.Directorate).UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.DirectorateId).HasColumnName("DirectorateID");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MaxGrade).UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.MinGrade).UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Name).UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Region).UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        modelBuilder.Entity<AllSchoolsBackup>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("AllSchoolsBackup");

            entity.Property(e => e.BteclabsCount).HasColumnName("BTECLabsCount");
            entity.Property(e => e.Directorate).UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MaxGrade).UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.MinGrade).UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Name).UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.NationalId).HasColumnName("NationalID");
            entity.Property(e => e.Region).UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.ToTable("Brand");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name).UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        modelBuilder.Entity<Datashow>(entity =>
        {
            entity.ToTable("Datashow");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.BrandId).HasColumnName("BrandID");
            entity.Property(e => e.LabId).HasColumnName("LabID");
            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

            entity.HasOne(d => d.Brand).WithMany(p => p.Datashows)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK_Datashow_Brand");

            entity.HasOne(d => d.Lab).WithMany(p => p.Datashows)
                .HasForeignKey(d => d.LabId)
                .HasConstraintName("FK_Datashow_Lab");

            entity.HasOne(d => d.Model).WithMany(p => p.Datashows)
                .HasForeignKey(d => d.ModelId)
                .HasConstraintName("FK_Datashow_Model");

            entity.HasOne(d => d.School).WithMany(p => p.Datashows)
                .HasForeignKey(d => d.SchoolId)
                .HasConstraintName("FK_Datashow_School");
        });

        modelBuilder.Entity<Desktop>(entity =>
        {
            entity.ToTable("Desktop");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Barcode).UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.BrandId).HasColumnName("BrandID");
            entity.Property(e => e.LabId).HasColumnName("LabID");
            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.Processor)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Ram)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("RAM");
            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");
            entity.Property(e => e.SerialNumber).UseCollation("SQL_Latin1_General_CP1_CI_AS");

            entity.HasOne(d => d.Brand).WithMany(p => p.Desktops)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK_Desktop_Brand");

            entity.HasOne(d => d.Lab).WithMany(p => p.Desktops)
                .HasForeignKey(d => d.LabId)
                .HasConstraintName("FK_Desktop_Lab");

            entity.HasOne(d => d.Modell).WithMany(p => p.Desktops)
                .HasForeignKey(d => d.ModelId)
                .HasConstraintName("FK_Desktop_Model");

            entity.HasOne(d => d.School).WithMany(p => p.Desktops)
                .HasForeignKey(d => d.SchoolId)
                .HasConstraintName("FK_Desktop_School");
        });

        modelBuilder.Entity<Directorate>(entity =>
        {
            entity.ToTable("Directorate");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ItdepartmentHead).HasColumnName("ITDepartmentHead");
            entity.Property(e => e.Name).UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");

            entity.HasOne(d => d.Region).WithMany(p => p.Directorates)
                .HasForeignKey(d => d.RegionId)
                .HasConstraintName("FK_Directorate_Region");
        });

        modelBuilder.Entity<DirectorateBackup>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("DirectorateBackup");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
        });

        modelBuilder.Entity<InteractiveBoard>(entity =>
        {
            entity.ToTable("InteractiveBoard");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.BrandId).HasColumnName("BrandID");
            entity.Property(e => e.LabId).HasColumnName("LabID");
            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

            entity.HasOne(d => d.Brand).WithMany(p => p.InteractiveBoards)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK_InteractiveBoard_Brand");

            entity.HasOne(d => d.Lab).WithMany(p => p.InteractiveBoards)
                .HasForeignKey(d => d.LabId)
                .HasConstraintName("FK_InteractiveBoard_Lab");

            entity.HasOne(d => d.Model).WithMany(p => p.InteractiveBoards)
                .HasForeignKey(d => d.ModelId)
                .HasConstraintName("FK_InteractiveBoard_Model");

            entity.HasOne(d => d.School).WithMany(p => p.InteractiveBoards)
                .HasForeignKey(d => d.SchoolId)
                .HasConstraintName("FK_InteractiveBoard_School");
        });

        modelBuilder.Entity<Lab>(entity =>
        {
            entity.ToTable("Lab");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");

            entity.HasOne(d => d.School).WithMany(p => p.Labs)
                .HasForeignKey(d => d.SchoolId)
                .HasConstraintName("FK_Lab_School");
        });

        modelBuilder.Entity<Laptop>(entity =>
        {
            entity.ToTable("Laptop");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Barcode).UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.BrandId).HasColumnName("BrandID");
            entity.Property(e => e.LabId).HasColumnName("LabID");
            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.Processor).UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Ram)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("RAM");
            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");
            entity.Property(e => e.SerialNumber).UseCollation("SQL_Latin1_General_CP1_CI_AS");

            entity.HasOne(d => d.Brand).WithMany(p => p.Laptops)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK_Laptop_Brand");

            entity.HasOne(d => d.Lab).WithMany(p => p.Laptops)
                .HasForeignKey(d => d.LabId)
                .HasConstraintName("FK_Laptop_Lab");

            entity.HasOne(d => d.Model).WithMany(p => p.Laptops)
                .HasForeignKey(d => d.ModelId)
                .HasConstraintName("FK_Laptop_Model");

            entity.HasOne(d => d.School).WithMany(p => p.Laptops)
                .HasForeignKey(d => d.SchoolId)
                .HasConstraintName("FK_Laptop_School");
        });

        modelBuilder.Entity<Modell>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Model");

            entity.ToTable("Modell");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.BrandId).HasColumnName("BrandID");
            entity.Property(e => e.DeviceType)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Name).UseCollation("SQL_Latin1_General_CP1_CI_AS");

            entity.HasOne(d => d.Brand).WithMany(p => p.Modells)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK_Model_Brand");
        });

        modelBuilder.Entity<NewAllDatum>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.AllPcsCount).HasColumnName("AllPCsCount");
            entity.Property(e => e.Bteclab).HasColumnName("BTECLab");
            entity.Property(e => e.Directorate).IsUnicode(false);
            entity.Property(e => e.DirectorateId).HasColumnName("DirectorateID");
            entity.Property(e => e.InteractiveBoardsLab1).HasColumnName("Interactive BoardsLab1");
            entity.Property(e => e.InteractiveBoardsLab2).HasColumnName("Interactive BoardsLab2");
            entity.Property(e => e.InteractiveBoardsLab3).HasColumnName("Interactive BoardsLab3");
            entity.Property(e => e.InteractiveBoardsLab4).HasColumnName("Interactive BoardsLab4");
            entity.Property(e => e.InteractiveBoardsLab5).HasColumnName("Interactive BoardsLab5");
            entity.Property(e => e.InteractiveBoardsLab6).HasColumnName("Interactive BoardsLab6");
            entity.Property(e => e.InteractiveBoardsLab7).HasColumnName("Interactive BoardsLab7");
            entity.Property(e => e.MaxGrade).IsUnicode(false);
            entity.Property(e => e.MinGrade).IsUnicode(false);
            entity.Property(e => e.NotInLabPcsCount).HasColumnName("NotInLabPCsCount");
            entity.Property(e => e.Region).IsUnicode(false);
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");
            entity.Property(e => e.SchoolName).IsUnicode(false);
        });

        modelBuilder.Entity<Printer>(entity =>
        {
            entity.ToTable("Printer");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.BrandId).HasColumnName("BrandID");
            entity.Property(e => e.LabId).HasColumnName("LabID");
            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

            entity.HasOne(d => d.Brand).WithMany(p => p.Printers)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK_Printer_Brand");

            entity.HasOne(d => d.Lab).WithMany(p => p.Printers)
                .HasForeignKey(d => d.LabId)
                .HasConstraintName("FK_Printer_Lab");

            entity.HasOne(d => d.Model).WithMany(p => p.Printers)
                .HasForeignKey(d => d.ModelId)
                .HasConstraintName("FK_Printer_Model");

            entity.HasOne(d => d.School).WithMany(p => p.Printers)
                .HasForeignKey(d => d.SchoolId)
                .HasConstraintName("FK_Printer_School");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.ToTable("Region");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name).UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        modelBuilder.Entity<School>(entity =>
        {
            entity.HasKey(e => e.NationalId);

            entity.ToTable("School");

            entity.Property(e => e.NationalId)
                .ValueGeneratedNever()
                .HasColumnName("NationalID");
            entity.Property(e => e.DirectorateId).HasColumnName("DirectorateID");
            entity.Property(e => e.MaxGrade)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.MinGrade)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Name).UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Password).UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.TechnicianId).HasColumnName("TechnicianID");

            entity.HasOne(d => d.Directorate).WithMany(p => p.Schools)
                .HasForeignKey(d => d.DirectorateId)
                .HasConstraintName("FK_School_Directorate");
        });

        modelBuilder.Entity<SupplementsTable>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SupplementsTable");

            entity.Property(e => e.AllPcsCount).HasColumnName("AllPCsCount");
            entity.Property(e => e.Bteclab).HasColumnName("BTECLab");
            entity.Property(e => e.Directorate)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.InteractiveBoardsLab1).HasColumnName("Interactive BoardsLab1");
            entity.Property(e => e.InteractiveBoardsLab2).HasColumnName("Interactive BoardsLab2");
            entity.Property(e => e.InteractiveBoardsLab3).HasColumnName("Interactive BoardsLab3");
            entity.Property(e => e.InteractiveBoardsLab4).HasColumnName("Interactive BoardsLab4");
            entity.Property(e => e.InteractiveBoardsLab5).HasColumnName("Interactive BoardsLab5");
            entity.Property(e => e.InteractiveBoardsLab6).HasColumnName("Interactive BoardsLab6");
            entity.Property(e => e.InteractiveBoardsLab7).HasColumnName("Interactive BoardsLab7");
            entity.Property(e => e.MaxGrade)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.MinGrade)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.NotInLabPcsCount).HasColumnName("NotInLabPCsCount");
            entity.Property(e => e.Region)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.SchoolId)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("SchoolID");
            entity.Property(e => e.SchoolName)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Unnamed48)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("Unnamed: 48");
            entity.Property(e => e.Unnamed49).HasColumnName("Unnamed: 49");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.MinistrialNumber).HasName("PK_Supervisor");

            entity.ToTable("User");

            entity.Property(e => e.MinistrialNumber).ValueGeneratedNever();
            entity.Property(e => e.DirectorateId).HasColumnName("DirectorateID");
            entity.Property(e => e.Name).UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Password).UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");

            entity.HasMany(d => d.Directorates).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserDirectorate",
                    r => r.HasOne<Directorate>().WithMany()
                        .HasForeignKey("DirectorateId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserDirec__Direc__6501FCD8"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserDirec__UserI__640DD89F"),
                    j =>
                    {
                        j.HasKey("UserId", "DirectorateId").HasName("PK__UserDire__23611A46809E8FA4");
                        j.ToTable("UserDirectorate");
                        j.IndexerProperty<int>("UserId").HasColumnName("UserID");
                        j.IndexerProperty<int>("DirectorateId").HasColumnName("DirectorateID");
                    });

            entity.HasMany(d => d.Schools).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserSchool",
                    r => r.HasOne<School>().WithMany()
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserSchoo__Schoo__61316BF4"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserSchoo__UserI__603D47BB"),
                    j =>
                    {
                        j.HasKey("UserId", "SchoolId").HasName("PK__UserScho__B4528ADBB6A7B91C");
                        j.ToTable("UserSchool");
                        j.IndexerProperty<int>("UserId").HasColumnName("UserID");
                        j.IndexerProperty<int>("SchoolId").HasColumnName("SchoolID");
                    });
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.ToTable("UserType");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
