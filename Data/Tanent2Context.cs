using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ESSTaskBatool.Data;

public partial class Tanent2Context : DbContext
{
    public Tanent2Context()
    {
    }

    public Tanent2Context(DbContextOptions<Tanent2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Tanent2info> Tanent2infos { get; set; }

    public virtual DbSet<Tanent2user> Tanent2users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=tanent2;user=Batool;password=Batoolhammoud@95", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_unicode_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Tanent2info>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tanent2info");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned zerofill")
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Phone).HasColumnName("phone");
        });

        modelBuilder.Entity<Tanent2user>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tanent2users");

            entity.HasIndex(e => e.Tanentid, "tanent2UserFK_idx").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Tanentid)
                .HasColumnType("int(10) unsigned zerofill")
                .HasColumnName("tanentid");

            entity.HasOne(d => d.Tanent).WithOne(p => p.Tanent2user)
                .HasForeignKey<Tanent2user>(d => d.Tanentid)
                .HasConstraintName("tanent2userFK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
