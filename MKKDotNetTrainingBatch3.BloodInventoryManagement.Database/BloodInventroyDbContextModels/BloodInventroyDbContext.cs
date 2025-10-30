using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MKKDotNetTrainingBatch3.BloodInventoryManagement.Database.BloodInventroyDbContextModels;

public partial class BloodInventroyDbContext : DbContext
{
    public BloodInventroyDbContext()
    {
    }

    public BloodInventroyDbContext(DbContextOptions<BloodInventroyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblBloodDonationHistory> TblBloodDonationHistories { get; set; }

    public virtual DbSet<TblBloodInventory> TblBloodInventories { get; set; }

    public virtual DbSet<TblBloodUsageHistory> TblBloodUsageHistories { get; set; }

    public virtual DbSet<TblDonor> TblDonors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=BloodInventoryManagement;User ID=sa;Password=P@ssw0rd;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBloodDonationHistory>(entity =>
        {
            entity.HasKey(e => new { e.DonorId, e.Date }).HasName("PK__tbl_Dona__AECF06401CC3D906");

            entity.ToTable("tbl_BloodDonationHistory");

            entity.Property(e => e.DonorId).HasColumnName("DonorID");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.BloodTypes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<TblBloodInventory>(entity =>
        {
            entity.HasKey(e => e.BloodTypes).HasName("PK__tbl_Bloo__2325476F792D9480");

            entity.ToTable("tbl_BloodInventory");

            entity.Property(e => e.BloodTypes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Count).HasDefaultValue(0);
        });

        modelBuilder.Entity<TblBloodUsageHistory>(entity =>
        {
            entity.HasKey(e => e.UsingId).HasName("PK__tbl_Bloo__1E057D7189221480");

            entity.ToTable("tbl_BloodUsageHistory");

            entity.Property(e => e.UsingId).HasColumnName("UsingID");
            entity.Property(e => e.BloodTypes)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TblDonor>(entity =>
        {
            entity.HasKey(e => new { e.DonorId, e.Email }).HasName("PK__tbl_Dono__052E3F98B52827D0");

            entity.ToTable("tbl_Donor");

            entity.Property(e => e.DonorId)
                .ValueGeneratedOnAdd()
                .HasColumnName("DonorID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.BloodType)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DonationCount).HasDefaultValue(0);
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PhoneNo).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
