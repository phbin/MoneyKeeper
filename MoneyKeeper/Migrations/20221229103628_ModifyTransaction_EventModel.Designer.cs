﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoneyKeeper.Models;

#nullable disable

namespace MoneyKeeper.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221229103628_ModifyTransaction_EventModel")]
    partial class ModifyTransaction_EventModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MoneyKeeper.Models.Budget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<int>("LimitAmount")
                        .HasColumnType("int");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("SpentAmount")
                        .HasColumnType("int");

                    b.Property<int>("WalletId")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("WalletId");

                    b.ToTable("Budget");
                });

            modelBuilder.Entity("MoneyKeeper.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("WalletId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("WalletId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("MoneyKeeper.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("WalletId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("WalletId");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("MoneyKeeper.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("WalletId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("EventId");

                    b.HasIndex("WalletId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("MoneyKeeper.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "test@gmail.com",
                            Password = "123123123"
                        },
                        new
                        {
                            Id = 2,
                            Email = "test2@gmail.com",
                            Password = "123123123"
                        },
                        new
                        {
                            Id = 3,
                            Email = "test3@gmail.com",
                            Password = "123123123"
                        },
                        new
                        {
                            Id = 4,
                            Email = "test4@gmail.com",
                            Password = "123123123"
                        });
                });

            modelBuilder.Entity("MoneyKeeper.Models.Wallet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Balance")
                        .HasColumnType("int");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Wallet");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Balance = 100000,
                            Icon = "",
                            IsDefault = true,
                            Name = "Ví",
                            Type = 0
                        },
                        new
                        {
                            Id = 2,
                            Balance = 200000,
                            Icon = "",
                            IsDefault = true,
                            Name = "Ví",
                            Type = 0
                        },
                        new
                        {
                            Id = 3,
                            Balance = 300000,
                            Icon = "",
                            IsDefault = true,
                            Name = "Ví",
                            Type = 0
                        },
                        new
                        {
                            Id = 4,
                            Balance = 400000,
                            Icon = "",
                            IsDefault = true,
                            Name = "Ví",
                            Type = 0
                        });
                });

            modelBuilder.Entity("MoneyKeeper.Models.WalletMember", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("WalletId")
                        .HasColumnType("int");

                    b.Property<DateTime>("JoinAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("UserId", "WalletId");

                    b.HasIndex("WalletId");

                    b.ToTable("WalletMember");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            WalletId = 1,
                            JoinAt = new DateTime(2022, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Role = 0
                        },
                        new
                        {
                            UserId = 2,
                            WalletId = 2,
                            JoinAt = new DateTime(2022, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Role = 0
                        },
                        new
                        {
                            UserId = 3,
                            WalletId = 3,
                            JoinAt = new DateTime(2022, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Role = 0
                        },
                        new
                        {
                            UserId = 4,
                            WalletId = 4,
                            JoinAt = new DateTime(2022, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Role = 0
                        });
                });

            modelBuilder.Entity("MoneyKeeper.Models.Budget", b =>
                {
                    b.HasOne("MoneyKeeper.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoneyKeeper.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoneyKeeper.Models.Wallet", "Wallet")
                        .WithMany()
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Creator");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("MoneyKeeper.Models.Category", b =>
                {
                    b.HasOne("MoneyKeeper.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoneyKeeper.Models.Wallet", "Wallet")
                        .WithMany()
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("MoneyKeeper.Models.Event", b =>
                {
                    b.HasOne("MoneyKeeper.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoneyKeeper.Models.Wallet", "Wallet")
                        .WithMany()
                        .HasForeignKey("WalletId");

                    b.Navigation("Creator");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("MoneyKeeper.Models.Transaction", b =>
                {
                    b.HasOne("MoneyKeeper.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoneyKeeper.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoneyKeeper.Models.Event", "Event")
                        .WithMany("Transactions")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoneyKeeper.Models.Wallet", "Wallet")
                        .WithMany()
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Creator");

                    b.Navigation("Event");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("MoneyKeeper.Models.WalletMember", b =>
                {
                    b.HasOne("MoneyKeeper.Models.Wallet", "Wallet")
                        .WithMany("WalletMembers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoneyKeeper.Models.User", "User")
                        .WithMany("WalletMembers")
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("MoneyKeeper.Models.Event", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("MoneyKeeper.Models.User", b =>
                {
                    b.Navigation("WalletMembers");
                });

            modelBuilder.Entity("MoneyKeeper.Models.Wallet", b =>
                {
                    b.Navigation("WalletMembers");
                });
#pragma warning restore 612, 618
        }
    }
}
