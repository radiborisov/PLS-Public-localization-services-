﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PLSServer.DBContext.Data;

namespace PLSServer.Migrations
{
    [DbContext(typeof(PLSDBContext))]
    [Migration("20190306094949_InitalCreate")]
    partial class InitalCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("PLSServer.DBContext.Data.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<decimal>("Altitude")
                        .HasColumnName("altitude");

                    b.Property<decimal>("Latitude")
                        .HasColumnName("latitude");

                    b.Property<decimal>("Longitude")
                        .HasColumnName("longitude");

                    b.Property<int>("UserId")
                        .HasColumnName("userid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("PLSServer.DBContext.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<bool>("IsOnline")
                        .HasColumnName("isonline");

                    b.Property<string>("PhoneNumber")
                        .HasColumnName("phonenumber")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PLSServer.DBContext.Data.Models.Location", b =>
                {
                    b.HasOne("PLSServer.DBContext.Data.Models.User", "User")
                        .WithMany("Locations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}