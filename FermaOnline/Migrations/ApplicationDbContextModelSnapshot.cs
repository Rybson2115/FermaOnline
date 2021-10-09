﻿// <auto-generated />
using System;
using FermaOnline.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FermaOnline.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FermaOnline.Models.CageFirstIndividualBodyWeight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CageId")
                        .HasColumnType("int");

                    b.Property<int>("ExperimentId")
                        .HasColumnType("int");

                    b.Property<float>("FirstIndividualBodyWeight")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("CFIBW");
                });

            modelBuilder.Entity("FermaOnline.Models.CageSurvey", b =>
                {
                    b.Property<int>("CageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CageQuantity")
                        .HasColumnType("int");

                    b.Property<int>("DeathCount")
                        .HasColumnType("int");

                    b.Property<float>("DifferenceInBodyWeight")
                        .HasColumnType("real");

                    b.Property<float>("GroupWeight")
                        .HasColumnType("real");

                    b.Property<float>("IndividualBodyWeight")
                        .HasColumnType("real");

                    b.Property<int>("SurveyId")
                        .HasColumnType("int");

                    b.Property<float>("WeightGainFromLastSurvey")
                        .HasColumnType("real");

                    b.Property<float>("WeightGainFromStart")
                        .HasColumnType("real");

                    b.HasKey("CageId");

                    b.HasIndex("SurveyId");

                    b.ToTable("Cage");
                });

            modelBuilder.Entity("FermaOnline.Models.Experiment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CageNumber")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Species")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Experiment");
                });

            modelBuilder.Entity("FermaOnline.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExperimentId")
                        .HasColumnType("int");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExperimentId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("FermaOnline.Models.Survey", b =>
                {
                    b.Property<int>("SurveyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("AverageBodyWeight")
                        .HasColumnType("real");

                    b.Property<float>("AverageWeightGainFromCages")
                        .HasColumnType("real");

                    b.Property<float>("AverageWeightGainFromLastSurvey")
                        .HasColumnType("real");

                    b.Property<int?>("DayOfLife")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("DaysFromFirstWeight")
                        .HasColumnType("int");

                    b.Property<int>("ExperimentId")
                        .HasColumnType("int");

                    b.Property<float>("FeedConversionRatio")
                        .HasColumnType("real");

                    b.Property<float?>("FeedIntakDaily")
                        .IsRequired()
                        .HasColumnType("real");

                    b.Property<float>("FeedIntakeWeekly")
                        .HasColumnType("real");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<float?>("LoculusFeedInTake")
                        .IsRequired()
                        .HasColumnType("real");

                    b.Property<int?>("LoculusQuantity")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<DateTime?>("SurveyDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.HasKey("SurveyId");

                    b.HasIndex("ExperimentId");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("FermaOnline.Models.CageSurvey", b =>
                {
                    b.HasOne("FermaOnline.Models.Survey", null)
                        .WithMany("Cages")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FermaOnline.Models.Image", b =>
                {
                    b.HasOne("FermaOnline.Models.Experiment", null)
                        .WithMany("Images")
                        .HasForeignKey("ExperimentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FermaOnline.Models.Survey", b =>
                {
                    b.HasOne("FermaOnline.Models.Experiment", null)
                        .WithMany("SurveysList")
                        .HasForeignKey("ExperimentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FermaOnline.Models.Experiment", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("SurveysList");
                });

            modelBuilder.Entity("FermaOnline.Models.Survey", b =>
                {
                    b.Navigation("Cages");
                });
#pragma warning restore 612, 618
        }
    }
}
