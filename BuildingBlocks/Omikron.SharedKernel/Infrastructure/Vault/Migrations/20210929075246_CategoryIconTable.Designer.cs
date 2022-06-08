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
    [Migration("20210929075246_CategoryIconTable")]
    partial class CategoryIconTable
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
                            CreatedAt = new DateTime(2021, 9, 29, 7, 52, 46, 400, DateTimeKind.Utc).AddTicks(3225),
                            HostName = "Omikron",
                            Identifier = "Omikron",
                            Logo = "images/applicita-software-logo.png",
                            ModifiedAt = new DateTime(2021, 9, 29, 7, 52, 46, 400, DateTimeKind.Utc).AddTicks(3239),
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

                    b.ToTable("AccountBalances");
                });

            modelBuilder.Entity("Omikron.SharedKernel.Infrastructure.Vault.Data.Models.Entities.CategoryIcon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("CategoryIcons");

                    b.HasData(
                        new
                        {
                            Id = new Guid("cc6ef233-0a6a-460b-b5b7-15ed4edbf026"),
                            Category = "bills",
                            CreatedAt = new DateTime(2021, 9, 29, 7, 52, 46, 382, DateTimeKind.Utc).AddTicks(2333),
                            Icon = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M19 22H5a3 3 0 0 1-3-3V3a1 1 0 0 1 1-1h14a1 1 0 0 1 1 1v12h4v4a3 3 0 0 1-3 3zm-1-5v2a1 1 0 0 0 2 0v-2h-2zm-2 3V4H4v15a1 1 0 0 0 1 1h11zM6 7h8v2H6V7zm0 4h8v2H6v-2zm0 4h5v2H6v-2z' fill='rgba(107,213,225,1)'/></svg>",
                            ModifiedAt = new DateTime(2021, 9, 29, 7, 52, 46, 382, DateTimeKind.Utc).AddTicks(2345)
                        },
                        new
                        {
                            Id = new Guid("565f5264-20fd-43ad-a7ec-203946313da4"),
                            Category = "eating_out",
                            CreatedAt = new DateTime(2021, 9, 29, 7, 52, 46, 382, DateTimeKind.Utc).AddTicks(3243),
                            Icon = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M14.268 12.146l-.854.854 7.071 7.071-1.414 1.414L12 14.415l-7.071 7.07-1.414-1.414 9.339-9.339c-.588-1.457.02-3.555 1.62-5.157 1.953-1.952 4.644-2.427 6.011-1.06s.892 4.058-1.06 6.01c-1.602 1.602-3.7 2.21-5.157 1.621zM4.222 3.808l6.717 6.717-2.828 2.829-3.89-3.89a4 4 0 0 1 0-5.656zM18.01 9.11c1.258-1.257 1.517-2.726 1.061-3.182-.456-.456-1.925-.197-3.182 1.06-1.257 1.258-1.516 2.727-1.06 3.183.455.455 1.924.196 3.181-1.061z'/></svg>",
                            ModifiedAt = new DateTime(2021, 9, 29, 7, 52, 46, 382, DateTimeKind.Utc).AddTicks(3244)
                        },
                        new
                        {
                            Id = new Guid("2d87cb42-f5f8-4000-985e-e7dafa092a01"),
                            Category = "entertainment",
                            CreatedAt = new DateTime(2021, 9, 29, 7, 52, 46, 382, DateTimeKind.Utc).AddTicks(3248),
                            Icon = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M2 3.993A1 1 0 0 1 2.992 3h18.016c.548 0 .992.445.992.993v16.014a1 1 0 0 1-.992.993H2.992A.993.993 0 0 1 2 20.007V3.993zM8 5v14h8V5H8zM4 5v2h2V5H4zm14 0v2h2V5h-2zM4 9v2h2V9H4zm14 0v2h2V9h-2zM4 13v2h2v-2H4zm14 0v2h2v-2h-2zM4 17v2h2v-2H4zm14 0v2h2v-2h-2z'/></svg>",
                            ModifiedAt = new DateTime(2021, 9, 29, 7, 52, 46, 382, DateTimeKind.Utc).AddTicks(3249)
                        },
                        new
                        {
                            Id = new Guid("2dcafdb1-88f6-4a5a-8b81-76280c5d804c"),
                            Category = "expenses",
                            CreatedAt = new DateTime(2021, 9, 29, 7, 52, 46, 382, DateTimeKind.Utc).AddTicks(3251),
                            Icon = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M12 22C6.477 22 2 17.523 2 12S6.477 2 12 2s10 4.477 10 10-4.477 10-10 10zm0-2a8 8 0 1 0 0-16 8 8 0 0 0 0 16zm-3.5-6H14a.5.5 0 1 0 0-1h-4a2.5 2.5 0 1 1 0-5h1V6h2v2h2.5v2H10a.5.5 0 1 0 0 1h4a2.5 2.5 0 1 1 0 5h-1v2h-2v-2H8.5v-2z'/></svg>",
                            ModifiedAt = new DateTime(2021, 9, 29, 7, 52, 46, 382, DateTimeKind.Utc).AddTicks(3251)
                        },
                        new
                        {
                            Id = new Guid("30d63765-d678-4cbf-8d54-945592d33921"),
                            Category = "general",
                            CreatedAt = new DateTime(2021, 9, 29, 7, 52, 46, 382, DateTimeKind.Utc).AddTicks(3253),
                            Icon = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M20 7V5H4v14h16v-2h-8a1 1 0 0 1-1-1V8a1 1 0 0 1 1-1h8zM3 3h18a1 1 0 0 1 1 1v16a1 1 0 0 1-1 1H3a1 1 0 0 1-1-1V4a1 1 0 0 1 1-1zm10 6v6h7V9h-7zm2 2h3v2h-3v-2z'/></svg>",
                            ModifiedAt = new DateTime(2021, 9, 29, 7, 52, 46, 382, DateTimeKind.Utc).AddTicks(3254)
                        },
                        new
                        {
                            Id = new Guid("6d47bde2-94a2-4799-a739-91ca21325006"),
                            Category = "groceries",
                            CreatedAt = new DateTime(2021, 9, 29, 7, 52, 46, 382, DateTimeKind.Utc).AddTicks(3256),
                            Icon = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M20 22H4a1 1 0 0 1-1-1V3a1 1 0 0 1 1-1h16a1 1 0 0 1 1 1v18a1 1 0 0 1-1 1zm-1-2V4H5v16h14zM9 6v2a3 3 0 0 0 6 0V6h2v2A5 5 0 0 1 7 8V6h2z' fill='rgba(255,193,74,1)'/></svg>",
                            ModifiedAt = new DateTime(2021, 9, 29, 7, 52, 46, 382, DateTimeKind.Utc).AddTicks(3257)
                        },
                        new
                        {
                            Id = new Guid("26db64b0-4669-444c-a88c-9308d3a4c4de"),
                            Category = "holidays",
                            CreatedAt = new DateTime(2021, 9, 29, 7, 52, 46, 382, DateTimeKind.Utc).AddTicks(3258),
                            Icon = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0H24V24H0z'/><path d='M18 23h-2v-1H8v1H6v-1H5c-1.105 0-2-.895-2-2V7c0-1.105.895-2 2-2h3V3c0-.552.448-1 1-1h6c.552 0 1 .448 1 1v2h3c1.105 0 2 .895 2 2v13c0 1.105-.895 2-2 2h-1v1zm1-16H5v13h14V7zm-9 2v9H8V9h2zm6 0v9h-2V9h2zm-2-5h-4v1h4V4z'/></svg>",
                            ModifiedAt = new DateTime(2021, 9, 29, 7, 52, 46, 382, DateTimeKind.Utc).AddTicks(3259)
                        },
                        new
                        {
                            Id = new Guid("57352454-0d30-4493-98eb-5a4019da5b87"),
                            Category = "internal",
                            CreatedAt = new DateTime(2021, 9, 29, 7, 52, 46, 382, DateTimeKind.Utc).AddTicks(3270),
                            Icon = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M12 20a8 8 0 1 0 0-16 8 8 0 0 0 0 16zm0 2C6.477 22 2 17.523 2 12S6.477 2 12 2s10 4.477 10 10-4.477 10-10 10zm0-6a4 4 0 1 0 0-8 4 4 0 0 0 0 8zm0 2a6 6 0 1 1 0-12 6 6 0 0 1 0 12zm0-4a2 2 0 1 1 0-4 2 2 0 0 1 0 4z'/></svg>",
                            ModifiedAt = new DateTime(2021, 9, 29, 7, 52, 46, 382, DateTimeKind.Utc).AddTicks(3271)
                        },
                        new
                        {
                            Id = new Guid("e7e98b30-9f25-4fda-b4b0-6b08dc30693a"),
                            Category = "personal_care",
                            CreatedAt = new DateTime(2021, 9, 29, 7, 52, 46, 382, DateTimeKind.Utc).AddTicks(3273),
                            Icon = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M17.841 15.659l.176.177.178-.177a2.25 2.25 0 0 1 3.182 3.182l-3.36 3.359-3.358-3.359a2.25 2.25 0 0 1 3.182-3.182zM12 14v2a6 6 0 0 0-6 6H4a8 8 0 0 1 7.75-7.996L12 14zm0-13c3.315 0 6 2.685 6 6a5.998 5.998 0 0 1-5.775 5.996L12 13c-3.315 0-6-2.685-6-6a5.998 5.998 0 0 1 5.775-5.996L12 1zm0 2C9.79 3 8 4.79 8 7s1.79 4 4 4 4-1.79 4-4-1.79-4-4-4z'/></svg>",
                            ModifiedAt = new DateTime(2021, 9, 29, 7, 52, 46, 382, DateTimeKind.Utc).AddTicks(3273)
                        },
                        new
                        {
                            Id = new Guid("c61accac-b207-4288-91ca-9efa25f4fcd4"),
                            Category = "personal_finance",
                            CreatedAt = new DateTime(2021, 9, 29, 7, 52, 46, 382, DateTimeKind.Utc).AddTicks(3275),
                            Icon = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M9.33 11.5h2.17A4.5 4.5 0 0 1 16 16H8.999L9 17h8v-1a5.578 5.578 0 0 0-.886-3H19a5 5 0 0 1 4.516 2.851C21.151 18.972 17.322 21 13 21c-2.761 0-5.1-.59-7-1.625L6 10.071A6.967 6.967 0 0 1 9.33 11.5zM5 19a1 1 0 0 1-1 1H2a1 1 0 0 1-1-1v-9a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1v9zM18 5a3 3 0 1 1 0 6 3 3 0 0 1 0-6zm-7-3a3 3 0 1 1 0 6 3 3 0 0 1 0-6z'/></svg>",
                            ModifiedAt = new DateTime(2021, 9, 29, 7, 52, 46, 382, DateTimeKind.Utc).AddTicks(3276)
                        },
                        new
                        {
                            Id = new Guid("f81a6509-217e-4807-84b0-7183cf7387a4"),
                            Category = "shopping",
                            CreatedAt = new DateTime(2021, 9, 29, 7, 52, 46, 382, DateTimeKind.Utc).AddTicks(3277),
                            Icon = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M12 2a6 6 0 0 1 6 6v1h4v2h-1.167l-.757 9.083a1 1 0 0 1-.996.917H4.92a1 1 0 0 1-.996-.917L3.166 11H2V9h4V8a6 6 0 0 1 6-6zm6.826 9H5.173l.667 8h12.319l.667-8zM13 13v4h-2v-4h2zm-4 0v4H7v-4h2zm8 0v4h-2v-4h2zm-5-9a4 4 0 0 0-3.995 3.8L8 8v1h8V8a4 4 0 0 0-3.8-3.995L12 4z' fill='rgba(149,138,248,1)'/></svg>",
                            ModifiedAt = new DateTime(2021, 9, 29, 7, 52, 46, 382, DateTimeKind.Utc).AddTicks(3278)
                        },
                        new
                        {
                            Id = new Guid("518b016b-c15b-4dce-9837-c6a23afcdd2f"),
                            Category = "transport",
                            CreatedAt = new DateTime(2021, 9, 29, 7, 52, 46, 382, DateTimeKind.Utc).AddTicks(3280),
                            Icon = "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M19 20H5v1a1 1 0 0 1-1 1H3a1 1 0 0 1-1-1v-9l2.513-6.702A2 2 0 0 1 6.386 4h11.228a2 2 0 0 1 1.873 1.298L22 12v9a1 1 0 0 1-1 1h-1a1 1 0 0 1-1-1v-1zM4.136 12h15.728l-2.25-6H6.386l-2.25 6zM6.5 17a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3zm11 0a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3z'/></svg>",
                            ModifiedAt = new DateTime(2021, 9, 29, 7, 52, 46, 382, DateTimeKind.Utc).AddTicks(3281)
                        });
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

                    b.Property<string>("MerchantLogo")
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<int?>("AccountSource")
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
