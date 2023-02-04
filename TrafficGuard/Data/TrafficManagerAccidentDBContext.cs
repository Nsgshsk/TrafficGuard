using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TrafficGuard.Models;

namespace TrafficGuard.Data
{
    public partial class TrafficManagerAccidentDBContext : DbContext
    {
        public TrafficManagerAccidentDBContext()
        {
        }

        public TrafficManagerAccidentDBContext(DbContextOptions<TrafficManagerAccidentDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accident> Accidents { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<District> Districts { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;initial catalog=TrafficManagerAccidentDB; trusted_connection=yes;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accident>(entity =>
            {
                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Accidents)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Accidents_Locations");
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasOne(d => d.City)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Districts_Cities");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasOne(d => d.District)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Locations_Districts");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
