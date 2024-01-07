﻿// <auto-generated />
using System;
using Dnd_Inventory_DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Dnd_Inventory_DAL.Migrations
{
    [DbContext(typeof(SessionDbContext))]
    [Migration("20240105200044_AddedInventoryAndItem")]
    partial class AddedInventoryAndItem
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Dnd_Inventory_DAL.Entities.Inventory", b =>
                {
                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("SessionId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.HasKey("ItemId", "SessionId", "UserId");

                    b.HasIndex("SessionId", "UserId");

                    b.ToTable("inventories");
                });

            modelBuilder.Entity("Dnd_Inventory_DAL.Entities.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("InventoryItemId")
                        .HasColumnType("int");

                    b.Property<int?>("InventorySessionId")
                        .HasColumnType("int");

                    b.Property<string>("InventoryUserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int?>("SessionId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<float>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("InventoryItemId", "InventorySessionId", "InventoryUserId");

                    b.ToTable("items");
                });

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

            modelBuilder.Entity("Dnd_Inventory_DAL.Entities.Inventory", b =>
                {
                    b.HasOne("Dnd_Inventory_DAL.Entities.Item", null)
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dnd_Inventory_DAL.Entities.SessionUsers", null)
                        .WithMany()
                        .HasForeignKey("SessionId", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dnd_Inventory_DAL.Entities.Item", b =>
                {
                    b.HasOne("Dnd_Inventory_DAL.Entities.Inventory", null)
                        .WithMany("Items")
                        .HasForeignKey("InventoryItemId", "InventorySessionId", "InventoryUserId");
                });

            modelBuilder.Entity("Dnd_Inventory_DAL.Entities.Inventory", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
