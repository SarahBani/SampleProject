﻿// <auto-generated />
using System;
using Core.DomainModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Core.DomainModel.Migrations
{
    [DbContext(typeof(SampleDataBaseContext))]
    [Migration("20190924175041_Add Branch & Country")]
    partial class AddBranchCountry
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.DomainModel.Entities.Bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte?>("Grade")
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.ToTable("Bank");
                });

            modelBuilder.Entity("Core.DomainModel.Entities.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BankId");

                    b.Property<int>("Code");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.ToTable("Branch");
                });

            modelBuilder.Entity("Core.DomainModel.Entities.Country", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("Core.DomainModel.Entities.Branch", b =>
                {
                    b.HasOne("Core.DomainModel.Entities.Bank", "Bank")
                        .WithMany("Branches")
                        .HasForeignKey("BankId")
                        .HasConstraintName("FK_Branch_Bank")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("Core.DomainModel.Entities.Address", "Address", b1 =>
                        {
                            b1.Property<int>("BranchId");

                            b1.Property<string>("BlockNo")
                                .IsRequired()
                                .HasColumnName("ShippingBlockNo")
                                .HasMaxLength(20);

                            b1.Property<string>("CityName")
                                .IsRequired()
                                .HasColumnName("ShippingCity")
                                .HasMaxLength(60);

                            b1.Property<short?>("CountryId");

                            b1.Property<string>("PostalCode")
                                .HasColumnName("PostalCode")
                                .HasMaxLength(10);

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnName("ShippingStreet")
                                .HasMaxLength(200);

                            b1.HasKey("BranchId");

                            b1.HasIndex("CountryId");

                            b1.ToTable("Address");

                            b1.HasOne("Core.DomainModel.Entities.Branch")
                                .WithOne("Address")
                                .HasForeignKey("Core.DomainModel.Entities.Address", "BranchId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.HasOne("Core.DomainModel.Entities.Country", "Country")
                                .WithMany()
                                .HasForeignKey("CountryId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
