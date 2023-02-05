﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrafficGuard.Data;

#nullable disable

namespace TrafficGuard.Migrations
{
    [DbContext(typeof(TrafficManagerAccidentDBContext))]
    [Migration("20230205185527_DefaultNow")]
    partial class DefaultNow
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TrafficGuard.Models.Accident", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("LocationId")
                        .HasColumnType("int")
                        .HasColumnName("Location_Id");

                    b.Property<int?>("NumVehicles")
                        .HasColumnType("int");

                    b.Property<string>("PathToFiles")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("TrustWorthyRating")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Accidents");
                });

            modelBuilder.Entity("TrafficGuard.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("TrafficGuard.Models.District", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CityId")
                        .HasColumnType("int")
                        .HasColumnName("City_Id");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("TrafficGuard.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DistrictId")
                        .HasColumnType("int")
                        .HasColumnName("District_Id");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("decimal(9,6)");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("decimal(9,6)");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("TrafficGuard.Models.Accident", b =>
                {
                    b.HasOne("TrafficGuard.Models.Location", "Location")
                        .WithMany("Accidents")
                        .HasForeignKey("LocationId")
                        .IsRequired()
                        .HasConstraintName("FK_Accidents_Locations");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("TrafficGuard.Models.District", b =>
                {
                    b.HasOne("TrafficGuard.Models.City", "City")
                        .WithMany("Districts")
                        .HasForeignKey("CityId")
                        .IsRequired()
                        .HasConstraintName("FK_Districts_Cities");

                    b.Navigation("City");
                });

            modelBuilder.Entity("TrafficGuard.Models.Location", b =>
                {
                    b.HasOne("TrafficGuard.Models.District", "District")
                        .WithMany("Locations")
                        .HasForeignKey("DistrictId")
                        .IsRequired()
                        .HasConstraintName("FK_Locations_Districts");

                    b.Navigation("District");
                });

            modelBuilder.Entity("TrafficGuard.Models.City", b =>
                {
                    b.Navigation("Districts");
                });

            modelBuilder.Entity("TrafficGuard.Models.District", b =>
                {
                    b.Navigation("Locations");
                });

            modelBuilder.Entity("TrafficGuard.Models.Location", b =>
                {
                    b.Navigation("Accidents");
                });
#pragma warning restore 612, 618
        }
    }
}
