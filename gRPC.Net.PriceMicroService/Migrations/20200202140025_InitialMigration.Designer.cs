﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using gRPC.Net.PriceMicroService.Storage;

namespace gRPC.Net.PriceMicroService.Migrations
{
    [DbContext(typeof(PriceContext))]
    [Migration("20200202140025_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1");

            modelBuilder.Entity("gRPC.Net.PriceMicroService.Entities.ProductPrice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("ProductPrices");
                });
#pragma warning restore 612, 618
        }
    }
}