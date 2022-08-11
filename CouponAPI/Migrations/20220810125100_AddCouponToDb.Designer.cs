﻿// <auto-generated />
using System;
using CouponAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CouponAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220810125100_AddCouponToDb")]
    partial class AddCouponToDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CouponAPI.Models.Coupon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Percent")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Coupons");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsActive = true,
                            Name = "10OFF",
                            Percent = 10
                        },
                        new
                        {
                            Id = 2,
                            IsActive = false,
                            Name = "20OFF",
                            Percent = 20
                        },
                        new
                        {
                            Id = 3,
                            IsActive = false,
                            Name = "30OFF",
                            Percent = 30
                        },
                        new
                        {
                            Id = 4,
                            IsActive = false,
                            Name = "50OFF",
                            Percent = 50
                        });
                });
#pragma warning restore 612, 618
        }
    }
}