﻿// <auto-generated />
using System;
using FineBudget;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FineBudget.Migrations
{
    [DbContext(typeof(BudgetContext))]
    partial class BudgetContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Models.DbModels.MainModels.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Balance")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Models.DbModels.MainModels.Asset", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AssetType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("Models.DbModels.MainModels.Cost", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AssetId")
                        .HasColumnType("uuid");

                    b.Property<int>("CostCategory")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("LiabilityId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Required")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TransactionNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("AssetId");

                    b.HasIndex("LiabilityId");

                    b.ToTable("Costs");
                });

            modelBuilder.Entity("Models.DbModels.MainModels.Income", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AssetId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("IncomeCategory")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("LiabilityId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TransactionNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("AssetId");

                    b.HasIndex("LiabilityId");

                    b.ToTable("Incomes");
                });

            modelBuilder.Entity("Models.DbModels.MainModels.Liability", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("LiabilityType")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Liabilities");
                });

            modelBuilder.Entity("Models.DbModels.MainModels.Cost", b =>
                {
                    b.HasOne("Models.DbModels.MainModels.Account", "Account")
                        .WithMany("Costs")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.DbModels.MainModels.Asset", "Asset")
                        .WithMany("Costs")
                        .HasForeignKey("AssetId");

                    b.HasOne("Models.DbModels.MainModels.Liability", "Liability")
                        .WithMany("Costs")
                        .HasForeignKey("LiabilityId");

                    b.Navigation("Account");

                    b.Navigation("Asset");

                    b.Navigation("Liability");
                });

            modelBuilder.Entity("Models.DbModels.MainModels.Income", b =>
                {
                    b.HasOne("Models.DbModels.MainModels.Account", "Account")
                        .WithMany("Incomes")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.DbModels.MainModels.Asset", "Asset")
                        .WithMany("Incomes")
                        .HasForeignKey("AssetId");

                    b.HasOne("Models.DbModels.MainModels.Liability", "Liability")
                        .WithMany("Incomes")
                        .HasForeignKey("LiabilityId");

                    b.Navigation("Account");

                    b.Navigation("Asset");

                    b.Navigation("Liability");
                });

            modelBuilder.Entity("Models.DbModels.MainModels.Account", b =>
                {
                    b.Navigation("Costs");

                    b.Navigation("Incomes");
                });

            modelBuilder.Entity("Models.DbModels.MainModels.Asset", b =>
                {
                    b.Navigation("Costs");

                    b.Navigation("Incomes");
                });

            modelBuilder.Entity("Models.DbModels.MainModels.Liability", b =>
                {
                    b.Navigation("Costs");

                    b.Navigation("Incomes");
                });
#pragma warning restore 612, 618
        }
    }
}
