﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

namespace Persistence.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200601085421_ActivitiesPopulated")]
    partial class ActivitiesPopulated
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Activity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Venue")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Activities");

                    b.HasData(
                        new
                        {
                            Id = new Guid("13e3b4c2-b880-4aa8-8e0c-33f4a5b45303"),
                            Category = "drinks",
                            City = "London",
                            Date = new DateTime(2020, 4, 1, 14, 54, 20, 668, DateTimeKind.Local).AddTicks(7647),
                            Description = "Activity 2 months ago",
                            Title = "Past Activity 1",
                            Venue = "Pub"
                        },
                        new
                        {
                            Id = new Guid("0b5c031d-00e4-4728-a9ec-b6506e07570e"),
                            Category = "culture",
                            City = "Paris",
                            Date = new DateTime(2020, 5, 1, 14, 54, 20, 669, DateTimeKind.Local).AddTicks(8341),
                            Description = "Activity 1 month ago",
                            Title = "Past Activity 2",
                            Venue = "Louvre"
                        },
                        new
                        {
                            Id = new Guid("4a3948f8-8333-47c6-adcf-680c62de2fbd"),
                            Category = "culture",
                            City = "London",
                            Date = new DateTime(2020, 7, 1, 14, 54, 20, 669, DateTimeKind.Local).AddTicks(8420),
                            Description = "Activity 1 month in future",
                            Title = "Future Activity 1",
                            Venue = "Natural History Museum"
                        },
                        new
                        {
                            Id = new Guid("eba2380c-2937-4749-8538-56391e3c7b4d"),
                            Category = "music",
                            City = "London",
                            Date = new DateTime(2020, 8, 1, 14, 54, 20, 669, DateTimeKind.Local).AddTicks(8425),
                            Description = "Activity 2 months in future",
                            Title = "Future Activity 2",
                            Venue = "O2 Arena"
                        },
                        new
                        {
                            Id = new Guid("23ead8f4-a6d5-421c-b872-2eb8530c2c67"),
                            Category = "drinks",
                            City = "London",
                            Date = new DateTime(2020, 9, 1, 14, 54, 20, 669, DateTimeKind.Local).AddTicks(8429),
                            Description = "Activity 3 months in future",
                            Title = "Future Activity 3",
                            Venue = "Another pub"
                        },
                        new
                        {
                            Id = new Guid("8a77952c-0c13-4b97-8b89-c548d8ba56e2"),
                            Category = "drinks",
                            City = "London",
                            Date = new DateTime(2020, 10, 1, 14, 54, 20, 669, DateTimeKind.Local).AddTicks(8432),
                            Description = "Activity 4 months in future",
                            Title = "Future Activity 4",
                            Venue = "Yet another pub"
                        },
                        new
                        {
                            Id = new Guid("be3b1404-7aa9-4508-ac3c-229fb307b27e"),
                            Category = "drinks",
                            City = "London",
                            Date = new DateTime(2020, 11, 1, 14, 54, 20, 669, DateTimeKind.Local).AddTicks(8448),
                            Description = "Activity 5 months in future",
                            Title = "Future Activity 5",
                            Venue = "Just another pub"
                        },
                        new
                        {
                            Id = new Guid("4080650d-c859-431e-9138-7e5f10f16036"),
                            Category = "music",
                            City = "London",
                            Date = new DateTime(2020, 12, 1, 14, 54, 20, 669, DateTimeKind.Local).AddTicks(8452),
                            Description = "Activity 6 months in future",
                            Title = "Future Activity 6",
                            Venue = "Roundhouse Camden"
                        },
                        new
                        {
                            Id = new Guid("d3b8642e-2c30-4c68-ab44-496e447a24dc"),
                            Category = "travel",
                            City = "London",
                            Date = new DateTime(2021, 1, 1, 14, 54, 20, 669, DateTimeKind.Local).AddTicks(8455),
                            Description = "Activity 2 months ago",
                            Title = "Future Activity 7",
                            Venue = "Somewhere on the Thames"
                        },
                        new
                        {
                            Id = new Guid("1876c616-63fc-4715-9c16-b0c86cdc360c"),
                            Category = "film",
                            City = "London",
                            Date = new DateTime(2021, 2, 1, 14, 54, 20, 669, DateTimeKind.Local).AddTicks(8459),
                            Description = "Activity 8 months in future",
                            Title = "Future Activity 8",
                            Venue = "Cinema"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}