﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Omikron.SharedKernel.Infrastructure.Vault.Data;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    [DbContext(typeof(VaultServiceDatabaseContext))]
    [Migration("20210820084104_RelationsFix")]
    partial class RelationsFix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("vault")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Omikron.VaultService.Domain.Models.Entities.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IdentificationNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LoanType")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Provider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Source")
                        .HasColumnType("int");

                    b.Property<int?>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Omikron.VaultService.Domain.Models.Entities.Property", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("MortgageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Postcode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MortgageId");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("Omikron.VaultService.Domain.Models.Entities.VaultItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AccountExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("AccountIdentificationNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccountProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AccountType")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("HostId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ItemType")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("VaultItems");
                });

            modelBuilder.Entity("Omikron.VaultService.Domain.Models.Entities.VaultItemValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("PropertyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<Guid?>("VehicleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("PropertyId");

                    b.HasIndex("VehicleId");

                    b.ToTable("VaultItemValues");
                });

            modelBuilder.Entity("Omikron.VaultService.Domain.Models.Entities.Vehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FinancialAgreementId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mileage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Registration")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FinancialAgreementId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("Omikron.VaultService.Domain.Models.Entities.Property", b =>
                {
                    b.HasOne("Omikron.VaultService.Domain.Models.Entities.Account", "Mortgage")
                        .WithMany("Properties")
                        .HasForeignKey("MortgageId");

                    b.Navigation("Mortgage");
                });

            modelBuilder.Entity("Omikron.VaultService.Domain.Models.Entities.VaultItemValue", b =>
                {
                    b.HasOne("Omikron.VaultService.Domain.Models.Entities.Account", "Account")
                        .WithMany("Values")
                        .HasForeignKey("AccountId");

                    b.HasOne("Omikron.VaultService.Domain.Models.Entities.Property", "Property")
                        .WithMany("Values")
                        .HasForeignKey("PropertyId");

                    b.HasOne("Omikron.VaultService.Domain.Models.Entities.Vehicle", "Vehicle")
                        .WithMany("Values")
                        .HasForeignKey("VehicleId");

                    b.Navigation("Account");

                    b.Navigation("Property");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("Omikron.VaultService.Domain.Models.Entities.Vehicle", b =>
                {
                    b.HasOne("Omikron.VaultService.Domain.Models.Entities.Account", "FinancialAgreement")
                        .WithMany("Vehicles")
                        .HasForeignKey("FinancialAgreementId");

                    b.Navigation("FinancialAgreement");
                });

            modelBuilder.Entity("Omikron.VaultService.Domain.Models.Entities.Account", b =>
                {
                    b.Navigation("Properties");

                    b.Navigation("Values");

                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("Omikron.VaultService.Domain.Models.Entities.Property", b =>
                {
                    b.Navigation("Values");
                });

            modelBuilder.Entity("Omikron.VaultService.Domain.Models.Entities.Vehicle", b =>
                {
                    b.Navigation("Values");
                });
#pragma warning restore 612, 618
        }
    }
}
