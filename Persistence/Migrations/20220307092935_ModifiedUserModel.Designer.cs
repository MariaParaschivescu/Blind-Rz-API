﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.Context;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(BlindRZContext))]
    [Migration("20220307092935_ModifiedUserModel")]
    partial class ModifiedUserModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DeviceSetting", b =>
                {
                    b.Property<Guid>("DevicesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SettingsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("DevicesId", "SettingsId");

                    b.HasIndex("SettingsId");

                    b.ToTable("DeviceSetting");
                });

            modelBuilder.Entity("Domain.Entities.Device", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ExternalIP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocalIP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("OpeningPercent")
                        .HasColumnType("float");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("Domain.Entities.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Domain.Entities.Program", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Programs");
                });

            modelBuilder.Entity("Domain.Entities.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("LocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RoomName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Domain.Entities.Setting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<double>("OpeningPercent")
                        .HasColumnType("float");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProgramRoom", b =>
                {
                    b.Property<Guid>("ProgramsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoomsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProgramsId", "RoomsId");

                    b.HasIndex("RoomsId");

                    b.ToTable("ProgramRoom");
                });

            modelBuilder.Entity("ProgramSetting", b =>
                {
                    b.Property<Guid>("ProgramsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SettingsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProgramsId", "SettingsId");

                    b.HasIndex("SettingsId");

                    b.ToTable("ProgramSetting");
                });

            modelBuilder.Entity("DeviceSetting", b =>
                {
                    b.HasOne("Domain.Entities.Device", null)
                        .WithMany()
                        .HasForeignKey("DevicesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Setting", null)
                        .WithMany()
                        .HasForeignKey("SettingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Location", b =>
                {
                    b.HasOne("Domain.Entities.User", null)
                        .WithMany("Locations")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Domain.Entities.Program", b =>
                {
                    b.HasOne("Domain.Entities.User", null)
                        .WithMany("Programs")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Domain.Entities.Room", b =>
                {
                    b.HasOne("Domain.Entities.Location", null)
                        .WithMany("Rooms")
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("ProgramRoom", b =>
                {
                    b.HasOne("Domain.Entities.Program", null)
                        .WithMany()
                        .HasForeignKey("ProgramsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Room", null)
                        .WithMany()
                        .HasForeignKey("RoomsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProgramSetting", b =>
                {
                    b.HasOne("Domain.Entities.Program", null)
                        .WithMany()
                        .HasForeignKey("ProgramsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Setting", null)
                        .WithMany()
                        .HasForeignKey("SettingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Location", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("Locations");

                    b.Navigation("Programs");
                });
#pragma warning restore 612, 618
        }
    }
}
