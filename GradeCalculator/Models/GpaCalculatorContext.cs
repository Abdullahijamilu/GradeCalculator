using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GradeCalculator.Models;

public partial class GpaCalculatorContext : DbContext
{
    public GpaCalculatorContext()
    {
    }

    public GpaCalculatorContext(DbContextOptions<GpaCalculatorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CalculateGpa> CalculateGpas { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-O4AVJKA;Database=GpaCalculator;User Id=Abdullahi Onimisi;Password=Abdul131; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CalculateGpa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Calculat__3214EC0791BE4681");

            entity.ToTable("CalculateGpa");

            entity.Property(e => e.Gpa).HasColumnName("GPA");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Course__3214EC0744FB621E");

            entity.ToTable("Course");

            entity.Property(e => e.Grade)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
