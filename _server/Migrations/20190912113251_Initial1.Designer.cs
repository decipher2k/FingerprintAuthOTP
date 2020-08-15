﻿// <auto-generated />
using System;
using AOTA_Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AOTA_Server.Migrations
{
    [DbContext(typeof(BuildingContext))]
    [Migration("20190912113251_Initial1")]
    partial class Initial1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AotaData.Building", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("PlayerDataid");

                    b.Property<long>("PrefabID");

                    b.Property<long>("buildingStartedTimestamp");

                    b.Property<long>("idUser");

                    b.Property<bool>("isFinished");

                    b.Property<float>("x");

                    b.Property<float>("y");

                    b.Property<float>("z");

                    b.HasKey("id");

                    b.HasIndex("PlayerDataid");

                    b.ToTable("Building");
                });

            modelBuilder.Entity("AotaData.BuildingType", b =>
                {
                    b.Property<long>("PrefabID")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("BuildCostGold");

                    b.Property<long>("BuildCostIron");

                    b.Property<long>("BuildCostMultiplier");

                    b.Property<long>("BuildCostSilver");

                    b.Property<long>("BuildDuration");

                    b.Property<long>("BuildDurationMultipliere");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("PrefabID");

                    b.ToTable("BuildingType");
                });

            modelBuilder.Entity("AotaData.PlayerData", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<long?>("Resourcesid");

                    b.HasKey("id");

                    b.HasIndex("Resourcesid");

                    b.ToTable("PlayerData");
                });

            modelBuilder.Entity("AotaData.Resources", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("Gold");

                    b.Property<long>("Iron");

                    b.Property<long>("Mana");

                    b.Property<long>("Silver");

                    b.HasKey("id");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("AotaData.Session", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("lastUpdate");

                    b.Property<long?>("playerDataid");

                    b.Property<string>("sessionKey");

                    b.HasKey("id");

                    b.HasIndex("playerDataid");

                    b.ToTable("Session");
                });

            modelBuilder.Entity("AotaData.Building", b =>
                {
                    b.HasOne("AotaData.PlayerData")
                        .WithMany("buildings")
                        .HasForeignKey("PlayerDataid");
                });

            modelBuilder.Entity("AotaData.PlayerData", b =>
                {
                    b.HasOne("AotaData.Resources", "Resources")
                        .WithMany()
                        .HasForeignKey("Resourcesid");
                });

            modelBuilder.Entity("AotaData.Session", b =>
                {
                    b.HasOne("AotaData.PlayerData", "playerData")
                        .WithMany()
                        .HasForeignKey("playerDataid");
                });
#pragma warning restore 612, 618
        }
    }
}
