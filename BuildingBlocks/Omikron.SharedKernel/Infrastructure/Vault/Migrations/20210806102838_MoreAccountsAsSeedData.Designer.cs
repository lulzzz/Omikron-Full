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
    [Migration("20210806102838_MoreAccountsAsSeedData")]
    partial class MoreAccountsAsSeedData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("vault")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Omikron.VaultService.Domain.Models.Account", b =>
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

                    b.HasData(
                        new
                        {
                            Id = new Guid("0679fd05-4c37-4eec-a029-62138d8e82b1"),
                            CreatedAt = new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc),
                            Currency = "GBP",
                            ExternalId = new Guid("0679fd05-4c37-4eec-a029-62138d8e82b1"),
                            ModifiedAt = new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc),
                            Name = "Holiday",
                            OwnerId = "D15D234D-D886-4F14-B3B0-480CBBABF6B1",
                            Provider = "Santader",
                            Source = 1,
                            Type = 2
                        },
                        new
                        {
                            Id = new Guid("3e28b304-faa8-4942-8c9b-4cf6fb6c621c"),
                            CreatedAt = new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc),
                            Currency = "GBP",
                            ExternalId = new Guid("3e28b304-faa8-4942-8c9b-4cf6fb6c621c"),
                            ModifiedAt = new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc),
                            Name = "Car",
                            OwnerId = "D15D234D-D886-4F14-B3B0-480CBBABF6B1",
                            Provider = "Santader",
                            Source = 1,
                            Type = 2
                        },
                        new
                        {
                            Id = new Guid("61b49ed2-7b6a-46b3-bd20-efcbc914bcb1"),
                            CreatedAt = new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc),
                            Currency = "GBP",
                            ExternalId = new Guid("61b49ed2-7b6a-46b3-bd20-efcbc914bcb1"),
                            ModifiedAt = new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc),
                            Name = "House",
                            OwnerId = "D15D234D-D886-4F14-B3B0-480CBBABF6B1",
                            Provider = "Santader",
                            Source = 1,
                            Type = 2
                        },
                        new
                        {
                            Id = new Guid("769dd368-4d01-4aa1-80c7-17b5a82793bb"),
                            CreatedAt = new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc),
                            Currency = "GBP",
                            ExternalId = new Guid("769dd368-4d01-4aa1-80c7-17b5a82793bb"),
                            ModifiedAt = new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc),
                            Name = "Everyday",
                            OwnerId = "D15D234D-D886-4F14-B3B0-480CBBABF6B1",
                            Provider = "Santader",
                            Source = 1,
                            Type = 1
                        },
                        new
                        {
                            Id = new Guid("59a9612e-3c12-46f5-863e-982444f64129"),
                            CreatedAt = new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc),
                            Currency = "GBP",
                            ExternalId = new Guid("59a9612e-3c12-46f5-863e-982444f64129"),
                            ModifiedAt = new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc),
                            Name = "Everyday",
                            OwnerId = "D15D234D-D886-4F14-B3B0-480CBBABF6B1",
                            Provider = "Santader",
                            Source = 1,
                            Type = 3
                        },
                        new
                        {
                            Id = new Guid("9e4cb31c-60da-4e56-9efe-134a014114c6"),
                            CreatedAt = new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc),
                            Currency = "GBP",
                            ExternalId = new Guid("9e4cb31c-60da-4e56-9efe-134a014114c6"),
                            ModifiedAt = new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc),
                            Name = "Everyday",
                            OwnerId = "D15D234D-D886-4F14-B3B0-480CBBABF6B1",
                            Provider = "Santader",
                            Source = 1,
                            Type = 4
                        },
                        new
                        {
                            Id = new Guid("7cf66e79-115b-4775-9e24-0b90a40e510c"),
                            CreatedAt = new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc),
                            Currency = "GBP",
                            ExternalId = new Guid("7cf66e79-115b-4775-9e24-0b90a40e510c"),
                            ModifiedAt = new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc),
                            Name = "Everyday",
                            OwnerId = "D15D234D-D886-4F14-B3B0-480CBBABF6B1",
                            Provider = "Santader",
                            Source = 1,
                            Type = 5
                        },
                        new
                        {
                            Id = new Guid("6a9f3896-9901-4024-ab46-9ffe997cace3"),
                            CreatedAt = new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc),
                            Currency = "GBP",
                            ExternalId = new Guid("6a9f3896-9901-4024-ab46-9ffe997cace3"),
                            ModifiedAt = new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc),
                            Name = "Everyday",
                            OwnerId = "D15D234D-D886-4F14-B3B0-480CBBABF6B1",
                            Provider = "Santader",
                            Source = 1,
                            Type = 6
                        },
                        new
                        {
                            Id = new Guid("5d031d63-ab9a-4c01-b407-8770b2418825"),
                            CreatedAt = new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc),
                            Currency = "GBP",
                            ExternalId = new Guid("5d031d63-ab9a-4c01-b407-8770b2418825"),
                            ModifiedAt = new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc),
                            Name = "Everyday",
                            OwnerId = "D15D234D-D886-4F14-B3B0-480CBBABF6B1",
                            Provider = "Santader",
                            Source = 1,
                            Type = 7
                        },
                        new
                        {
                            Id = new Guid("0a4c0cbd-c03b-4206-a633-46fb118177c3"),
                            CreatedAt = new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc),
                            Currency = "GBP",
                            ExternalId = new Guid("0a4c0cbd-c03b-4206-a633-46fb118177c3"),
                            ModifiedAt = new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc),
                            Name = "Everyday",
                            OwnerId = "D15D234D-D886-4F14-B3B0-480CBBABF6B1",
                            Provider = "Santader",
                            Source = 1,
                            Type = 8
                        },
                        new
                        {
                            Id = new Guid("c641f883-c946-4735-9a80-e09bde294378"),
                            CreatedAt = new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc),
                            Currency = "GBP",
                            ExternalId = new Guid("c641f883-c946-4735-9a80-e09bde294378"),
                            ModifiedAt = new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc),
                            Name = "Everyday",
                            OwnerId = "D15D234D-D886-4F14-B3B0-480CBBABF6B1",
                            Provider = "Santader",
                            Source = 1,
                            Type = 9
                        });
                });

            modelBuilder.Entity("Omikron.VaultService.Domain.Models.AccountBalance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
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

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("AccountBalances");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2dd8db17-8bcf-4edb-b94e-a06f901d0558"),
                            AccountId = new Guid("0679fd05-4c37-4eec-a029-62138d8e82b1"),
                            Amount = 1321.23m,
                            CreatedAt = new DateTime(2021, 8, 6, 10, 28, 37, 972, DateTimeKind.Utc).AddTicks(8371),
                            EntryDate = new DateTime(2021, 8, 6, 10, 28, 37, 972, DateTimeKind.Utc).AddTicks(9977),
                            ModifiedAt = new DateTime(2021, 8, 6, 10, 28, 37, 972, DateTimeKind.Utc).AddTicks(8389),
                            Type = 2
                        },
                        new
                        {
                            Id = new Guid("0e9732a0-d596-4e93-8dcd-22ed9f685bba"),
                            AccountId = new Guid("0679fd05-4c37-4eec-a029-62138d8e82b1"),
                            Amount = 13223.23m,
                            CreatedAt = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2033),
                            EntryDate = new DateTime(2021, 8, 4, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2054),
                            ModifiedAt = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2036),
                            Type = 2
                        },
                        new
                        {
                            Id = new Guid("b2f2c6d8-c14c-4156-aec4-ecde51c1d74a"),
                            AccountId = new Guid("3e28b304-faa8-4942-8c9b-4cf6fb6c621c"),
                            Amount = 2223.23m,
                            CreatedAt = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2088),
                            EntryDate = new DateTime(2021, 8, 4, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2094),
                            ModifiedAt = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2089),
                            Type = 2
                        },
                        new
                        {
                            Id = new Guid("4d04f7a8-8e03-4563-abe5-16503b5e8051"),
                            AccountId = new Guid("61b49ed2-7b6a-46b3-bd20-efcbc914bcb1"),
                            Amount = 5223.23m,
                            CreatedAt = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2096),
                            EntryDate = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2100),
                            ModifiedAt = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2097),
                            Type = 2
                        },
                        new
                        {
                            Id = new Guid("92271498-4bb6-4079-975b-7df225e7575b"),
                            AccountId = new Guid("769dd368-4d01-4aa1-80c7-17b5a82793bb"),
                            Amount = 5223.23m,
                            CreatedAt = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2153),
                            EntryDate = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2158),
                            ModifiedAt = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2154),
                            Type = 2
                        },
                        new
                        {
                            Id = new Guid("2e97f1d5-fa57-41fe-84a1-e8348b69c75e"),
                            AccountId = new Guid("59a9612e-3c12-46f5-863e-982444f64129"),
                            Amount = 2265.23m,
                            CreatedAt = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2160),
                            EntryDate = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2164),
                            ModifiedAt = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2160),
                            Type = 2
                        },
                        new
                        {
                            Id = new Guid("8364a3f7-3aa0-4122-af43-1632e551bf6d"),
                            AccountId = new Guid("9e4cb31c-60da-4e56-9efe-134a014114c6"),
                            Amount = 52625.23m,
                            CreatedAt = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2165),
                            EntryDate = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2169),
                            ModifiedAt = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2166),
                            Type = 2
                        },
                        new
                        {
                            Id = new Guid("ab54d989-1ef7-4244-88c4-dbcc7fb810c7"),
                            AccountId = new Guid("7cf66e79-115b-4775-9e24-0b90a40e510c"),
                            Amount = 232625.23m,
                            CreatedAt = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2170),
                            EntryDate = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2174),
                            ModifiedAt = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2171),
                            Type = 2
                        },
                        new
                        {
                            Id = new Guid("c329ec9e-2663-4dad-8bc5-d898563b745d"),
                            AccountId = new Guid("6a9f3896-9901-4024-ab46-9ffe997cace3"),
                            Amount = -1325.23m,
                            CreatedAt = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2176),
                            EntryDate = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2185),
                            ModifiedAt = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2176),
                            Type = 2
                        },
                        new
                        {
                            Id = new Guid("d0febb10-b10d-456e-a959-d9af8ad70d13"),
                            AccountId = new Guid("5d031d63-ab9a-4c01-b407-8770b2418825"),
                            Amount = -11325.23m,
                            CreatedAt = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2186),
                            EntryDate = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2190),
                            ModifiedAt = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2187),
                            Type = 2
                        },
                        new
                        {
                            Id = new Guid("e94db63c-f3cf-4ed9-ac55-2fa77ecc59db"),
                            AccountId = new Guid("0a4c0cbd-c03b-4206-a633-46fb118177c3"),
                            Amount = -66325.23m,
                            CreatedAt = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2191),
                            EntryDate = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2195),
                            ModifiedAt = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2192),
                            Type = 2
                        },
                        new
                        {
                            Id = new Guid("2d7fef9c-b4d7-416d-81b7-95488878f86c"),
                            AccountId = new Guid("c641f883-c946-4735-9a80-e09bde294378"),
                            Amount = -26325.23m,
                            CreatedAt = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2196),
                            EntryDate = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2200),
                            ModifiedAt = new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2197),
                            Type = 2
                        });
                });

            modelBuilder.Entity("Omikron.VaultService.Domain.Models.AccountBalance", b =>
                {
                    b.HasOne("Omikron.VaultService.Domain.Models.Account", "Account")
                        .WithMany("AccountBalances")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Omikron.VaultService.Domain.Models.Account", b =>
                {
                    b.Navigation("AccountBalances");
                });
#pragma warning restore 612, 618
        }
    }
}