﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TradeUp.Domain;

#nullable disable

namespace TradeUp.Domain.Migrations
{
    [DbContext(typeof(TradeUpContext))]
    [Migration("20211219071932_AddResourceConsumer")]
    partial class AddResourceConsumer
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TradeUp.Domain.Consumer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ConsumptionFactor")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Consumers");
                });

            modelBuilder.Entity("TradeUp.Domain.Contributor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ContributionFactor")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Contributors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ContributionFactor = 1,
                            Name = "London",
                            Size = 100
                        },
                        new
                        {
                            Id = 2,
                            ContributionFactor = 1,
                            Name = "Manchester",
                            Size = 50
                        });
                });

            modelBuilder.Entity("TradeUp.Domain.Resource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<long>("CountAvailable")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("Resources");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CountAvailable = 100L,
                            Name = "Iron",
                            Price = 10.5
                        },
                        new
                        {
                            Id = 2,
                            CountAvailable = 50L,
                            Name = "Copper",
                            Price = 4.0
                        });
                });

            modelBuilder.Entity("TradeUp.Domain.ResourceConsumer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ConsumerId")
                        .HasColumnType("int");

                    b.Property<int>("Frequency")
                        .HasColumnType("int");

                    b.Property<int>("MaxConsumptionRange")
                        .HasColumnType("int");

                    b.Property<int>("MinConsumptionRange")
                        .HasColumnType("int");

                    b.Property<int>("ResourceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConsumerId");

                    b.HasIndex("ResourceId");

                    b.ToTable("ResourceConsumers");
                });

            modelBuilder.Entity("TradeUp.Domain.ResourceConsumptionHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<int>("ResourceConsumerId")
                        .HasColumnType("int");

                    b.Property<long>("ResourceCountAfterConsumption")
                        .HasColumnType("bigint");

                    b.Property<double>("ResourcePriceAtTimeOfConsumption")
                        .HasColumnType("double");

                    b.Property<long>("ResourcesConsumed")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ResourceConsumerId");

                    b.ToTable("ResourceConsumptionHistory");
                });

            modelBuilder.Entity("TradeUp.Domain.ResourceContributionHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<int>("ResourceContributorId")
                        .HasColumnType("int");

                    b.Property<long>("ResourceCount")
                        .HasColumnType("bigint");

                    b.Property<double>("ResourcePriceAtTimeOfContribution")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("ResourceContributorId");

                    b.ToTable("ResourceContributionHistory");
                });

            modelBuilder.Entity("TradeUp.Domain.ResourceContributor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ContributorId")
                        .HasColumnType("int");

                    b.Property<int>("Frequency")
                        .HasColumnType("int");

                    b.Property<int>("MaxContributionRange")
                        .HasColumnType("int");

                    b.Property<int>("MinContributionRange")
                        .HasColumnType("int");

                    b.Property<int>("ResourceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContributorId");

                    b.HasIndex("ResourceId");

                    b.ToTable("ResourceContributors");
                });

            modelBuilder.Entity("TradeUp.Domain.ResourceConsumer", b =>
                {
                    b.HasOne("TradeUp.Domain.Consumer", "Consumer")
                        .WithMany()
                        .HasForeignKey("ConsumerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TradeUp.Domain.Resource", "Resource")
                        .WithMany()
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Consumer");

                    b.Navigation("Resource");
                });

            modelBuilder.Entity("TradeUp.Domain.ResourceConsumptionHistory", b =>
                {
                    b.HasOne("TradeUp.Domain.ResourceConsumer", "ResourceConsumer")
                        .WithMany()
                        .HasForeignKey("ResourceConsumerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ResourceConsumer");
                });

            modelBuilder.Entity("TradeUp.Domain.ResourceContributionHistory", b =>
                {
                    b.HasOne("TradeUp.Domain.ResourceContributor", "ResourceContributor")
                        .WithMany()
                        .HasForeignKey("ResourceContributorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ResourceContributor");
                });

            modelBuilder.Entity("TradeUp.Domain.ResourceContributor", b =>
                {
                    b.HasOne("TradeUp.Domain.Contributor", "Contributor")
                        .WithMany()
                        .HasForeignKey("ContributorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TradeUp.Domain.Resource", "Resource")
                        .WithMany()
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contributor");

                    b.Navigation("Resource");
                });
#pragma warning restore 612, 618
        }
    }
}
