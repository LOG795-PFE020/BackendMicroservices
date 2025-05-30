﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Portfolio.Repositories;

#nullable disable

namespace Portfolio.Migrations
{
    [DbContext(typeof(WalletContext))]
    [Migration("20250417054501_InitialWallet")]
    partial class InitialWallet
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Portfolio.Domain.Wallet", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Wallet");
                });

            modelBuilder.Entity("Portfolio.Domain.Wallet", b =>
                {
                    b.OwnsOne("Portfolio.Domain.ValueObjects.Money", "Balance", b1 =>
                        {
                            b1.Property<string>("WalletId")
                                .HasColumnType("text");

                            b1.Property<decimal>("Value")
                                .HasColumnType("numeric")
                                .HasColumnName("Balance");

                            b1.HasKey("WalletId");

                            b1.ToTable("Wallet");

                            b1.WithOwner()
                                .HasForeignKey("WalletId");
                        });

                    b.OwnsMany("Portfolio.Domain.ValueObjects.ShareVolume", "Shares", b1 =>
                        {
                            b1.Property<string>("WalletId")
                                .HasColumnType("text");

                            b1.Property<string>("Symbol")
                                .HasColumnType("text")
                                .HasColumnName("Symbol");

                            b1.Property<int>("Volume")
                                .HasColumnType("integer")
                                .HasColumnName("Volume");

                            b1.HasKey("WalletId", "Symbol");

                            b1.ToTable("WalletShares", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("WalletId");
                        });

                    b.Navigation("Balance")
                        .IsRequired();

                    b.Navigation("Shares");
                });
#pragma warning restore 612, 618
        }
    }
}
