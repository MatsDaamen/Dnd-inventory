﻿// <auto-generated />
using System;
using Dnd_Inventory_DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Dnd_Inventory_DAL.Migrations
{
    [DbContext(typeof(InventoryDbContext))]
    partial class InventoryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Dnd_Inventory_DAL.Entities.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("Dnd_Inventory_DAL.Entities.SessionJoinKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<Guid>("JoinKey")
                        .HasColumnType("char(36)");

                    b.Property<int>("SessionId")
                        .HasColumnType("int");

                    b.Property<int>("UsesLeft")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("JoinKeys");
                });

            modelBuilder.Entity("Dnd_Inventory_DAL.Entities.SessionUsers", b =>
                {
                    b.Property<int>("SessionId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("SessionId", "UserId");

                    b.ToTable("SessionUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
