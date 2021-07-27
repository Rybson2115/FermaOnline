﻿// <auto-generated />
using System;
using FermaOnline.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FermaOnline.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210727102422_ExperimetnTwoNewPropMigration")]
    partial class ExperimetnTwoNewPropMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<float>("WeightGainFromLastSurvey")
                        .HasColumnType("real");

                    b.Property<float>("WeightGainFromStart")
                        .HasColumnType("real");

                    b.HasKey("CageId");

                    b.ToTable("Cage");
                });

            modelBuilder.Entity("FermaOnline.Models.Experiment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("AFirstIndividualBodyWeight")
                        .HasColumnType("real");

                    b.Property<float>("BFirstIndividualBodyWeight")
                        .HasColumnType("real");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Experiment");
                });

            modelBuilder.Entity("FermaOnline.Models.Survey", b =>
                {
                    b.Property<int>("SurveyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ACageId")
                        .HasColumnType("int");

                    b.Property<float>("AverageBodyWeight")
                        .HasColumnType("real");

                    b.Property<float>("AverageWeightGain")
                        .HasColumnType("real");

                    b.Property<float>("AverageWeightGainFromStart")
                        .HasColumnType("real");

                    b.Property<int?>("BCageId")
                        .HasColumnType("int");

                    b.Property<int>("DayOfLife")
                        .HasColumnType("int");

                    b.Property<int>("DaysFromFirstWeight")
                        .HasColumnType("int");

                    b.Property<int?>("ExperimentId")
                        .HasColumnType("int");

                    b.Property<int>("ExperymentId")
                        .HasColumnType("int");

                    b.Property<float>("FeedConversionRatio")
                        .HasColumnType("real");

                    b.Property<float>("FeedIntakDaily")
                        .HasColumnType("real");

                    b.Property<float>("FeedIntakeWeekly")
                        .HasColumnType("real");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<float>("LoculusFeedInTake")
                        .HasColumnType("real");

                    b.Property<int>("LoculusQuantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("SurveyDate")
                        .HasColumnType("datetime2");

                    b.HasKey("SurveyId");

                    b.HasIndex("ACageId");

                    b.HasIndex("BCageId");

                    b.HasIndex("ExperimentId");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("FermaOnline.Models.Survey", b =>
                {
                    b.HasOne("FermaOnline.Models.CageSurvey", "A")
                        .WithMany()
                        .HasForeignKey("ACageId");

                    b.HasOne("FermaOnline.Models.CageSurvey", "B")
                        .WithMany()
                        .HasForeignKey("BCageId");

                    b.HasOne("FermaOnline.Models.Experiment", null)
                        .WithMany("SurveysList")
                        .HasForeignKey("ExperimentId");

                    b.Navigation("A");

                    b.Navigation("B");
                });

            modelBuilder.Entity("FermaOnline.Models.Experiment", b =>
                {
                    b.Navigation("SurveysList");
                });
#pragma warning restore 612, 618
        }
    }
}
