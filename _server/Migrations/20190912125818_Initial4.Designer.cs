﻿// <auto-generated />
using AOTA_Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AOTA_Server.Migrations
{
    [DbContext(typeof(BuildingContext))]
    [Migration("20190912125818_Initial4")]
    partial class Initial4
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

                    b.Property<long>("PrefabID");

                    b.Property<long>("buildingStartedTimestamp");

                    b.Property<long>("idUser");

                    b.Property<bool>("isFinished");

                    b.Property<float>("x");

                    b.Property<float>("y");

                    b.Property<float>("z");

                    b.HasKey("id");

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

                    b.Property<long>("Gold");

                    b.Property<long>("Iron");

                    b.Property<long>("Mana");

                    b.Property<string>("Name");

                    b.Property<long>("Silver");

                    b.HasKey("id");

                    b.ToTable("PlayerData");
                });

            modelBuilder.Entity("AotaData.Resources", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("idUser");

                    b.HasKey("id");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("AotaData.Session", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("idUser");

                    b.Property<long>("lastUpdate");

                    b.Property<string>("sessionKey");

                    b.HasKey("id");

                    b.ToTable("Session");
                });
#pragma warning restore 612, 618
        }
    }
}
