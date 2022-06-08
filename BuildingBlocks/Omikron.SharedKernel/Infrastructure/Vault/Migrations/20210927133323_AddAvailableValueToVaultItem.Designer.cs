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
    [Migration("20210927133323_AddAvailableValueToVaultItem")]
    partial class AddAvailableValueToVaultItem
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("vault")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Omikron.SharedKernel.Infrastructure.Data.Model.Tenant", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AzureAssetStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CssFile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HostName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Identifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Tenant");

                    b.HasData(
                        new
                        {
                            Id = "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                            AzureAssetStatus = 4,
                            CreatedAt = new DateTime(2021, 9, 27, 13, 33, 23, 397, DateTimeKind.Utc).AddTicks(2247),
                            HostName = "Omikron",
                            Identifier = "Omikron",
                            Logo = "images/applicita-software-logo.png",
                            ModifiedAt = new DateTime(2021, 9, 27, 13, 33, 23, 397, DateTimeKind.Utc).AddTicks(2259),
                            Name = "Omikron Money Solution",
                            Status = 1,
                            Type = 1
                        });
                });

            modelBuilder.Entity("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BudAccountId")
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Provider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReferenceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Source")
                        .HasColumnType("int");

                    b.Property<int?>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.AccountBalance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("BalanceType")
                        .HasColumnType("int");

                    b.Property<string>("BudAccountId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("EntryDate")
                        .IsUnique();

                    b.ToTable("AccountBalances");
                });

            modelBuilder.Entity("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.PersonalItem", b =>
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

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FinancialAgreementId");

                    b.ToTable("PersonalItems");
                });

            modelBuilder.Entity("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.PersonalItemValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PersonalItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("PersonalItemId");

                    b.ToTable("PersonalItemValues");
                });

            modelBuilder.Entity("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.Property", b =>
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

            modelBuilder.Entity("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.PropertyValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PropertyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.ToTable("PropertyValues");
                });

            modelBuilder.Entity("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.RefreshHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("RowVersion")
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("RefreshHistories");
                });

            modelBuilder.Entity("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("BudTransactionId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("MerchantName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.VaultItem", b =>
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

                    b.Property<int>("AccountSource")
                        .HasColumnType("int");

                    b.Property<int?>("AccountType")
                        .HasColumnType("int");

                    b.Property<decimal>("AvailableValue")
                        .HasColumnType("decimal(18,2)");

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

                    b.Property<string>("ReferenceNumber")
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

            modelBuilder.Entity("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.Vehicle", b =>
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

            modelBuilder.Entity("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.VehicleValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("VehicleValues");
                });

            modelBuilder.Entity("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.AccountBalance", b =>
                {
                    b.HasOne("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.Account", "Account")
                        .WithMany("AccountBalances")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.PersonalItem", b =>
                {
                    b.HasOne("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.Account", "FinancialAgreement")
                        .WithMany("PersonalItems")
                        .HasForeignKey("FinancialAgreementId");

                    b.Navigation("FinancialAgreement");
                });

            modelBuilder.Entity("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.PersonalItemValue", b =>
                {
                    b.HasOne("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.PersonalItem", "PersonalItem")
                        .WithMany("PersonalItemValues")
                        .HasForeignKey("PersonalItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PersonalItem");
                });

            modelBuilder.Entity("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.Property", b =>
                {
                    b.HasOne("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.Account", "Mortgage")
                        .WithMany("Properties")
                        .HasForeignKey("MortgageId");

                    b.Navigation("Mortgage");
                });

            modelBuilder.Entity("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.PropertyValue", b =>
                {
                    b.HasOne("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.Property", "Property")
                        .WithMany("PropertyValues")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.Transaction", b =>
                {
                    b.HasOne("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.Vehicle", b =>
                {
                    b.HasOne("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.Account", "FinancialAgreement")
                        .WithMany("Vehicles")
                        .HasForeignKey("FinancialAgreementId");

                    b.Navigation("FinancialAgreement");
                });

            modelBuilder.Entity("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.VehicleValue", b =>
                {
                    b.HasOne("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.Vehicle", "Vehicle")
                        .WithMany("VehicleValues")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.Account", b =>
                {
                    b.Navigation("AccountBalances");

                    b.Navigation("PersonalItems");

                    b.Navigation("Properties");

                    b.Navigation("Transactions");

                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.PersonalItem", b =>
                {
                    b.Navigation("PersonalItemValues");
                });

            modelBuilder.Entity("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.Property", b =>
                {
                    b.Navigation("PropertyValues");
                });

            modelBuilder.Entity("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.Vehicle", b =>
                {
                    b.Navigation("VehicleValues");
                });
#pragma warning restore 612, 618
        }
    }
}
