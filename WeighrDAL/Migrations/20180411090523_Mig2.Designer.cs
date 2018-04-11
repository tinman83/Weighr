﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using WeighrDAL;

namespace WeighrDAL.Migrations
{
    [DbContext(typeof(WeighrContext))]
    [Migration("20180411090523_Mig2")]
    partial class Mig2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("WeighrDAL.Models.AccumulatedWeight", b =>
                {
                    b.Property<int>("AccumulatedWeightId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CurrentDate");

                    b.Property<DateTime>("StartDate");

                    b.Property<decimal>("Weight");

                    b.HasKey("AccumulatedWeightId");

                    b.ToTable("AccumulatedWeights");
                });

            modelBuilder.Entity("WeighrDAL.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DueDate");

                    b.Property<DateTime>("OrderDate");

                    b.Property<string>("OrderNumber");

                    b.Property<int>("Status");

                    b.Property<Guid>("rowguid");

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WeighrDAL.Models.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("OrderId");

                    b.Property<int>("ProductId");

                    b.Property<int>("Units");

                    b.HasKey("OrderDetailId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("WeighrDAL.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Density");

                    b.Property<decimal>("LowerLimit");

                    b.Property<string>("Name");

                    b.Property<string>("ProductCode");

                    b.Property<decimal>("TargetWeight");

                    b.Property<decimal>("UpperLimit");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WeighrDAL.Models.ScaleConfig", b =>
                {
                    b.Property<int>("ScaleConfigId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Gradient");

                    b.Property<decimal>("MaximumCapacity");

                    b.Property<decimal>("MinimumDivision");

                    b.Property<decimal>("Resolution");

                    b.Property<double>("YIntercept");

                    b.HasKey("ScaleConfigId");

                    b.ToTable("ScaleConfigs");
                });

            modelBuilder.Entity("WeighrDAL.Models.ScaleSetting", b =>
                {
                    b.Property<int>("ScaleSettingId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DecimalPointPosition");

                    b.Property<string>("DecimalPointPositionName");

                    b.Property<string>("DisplayUnits");

                    b.Property<bool>("PowerOnZero");

                    b.Property<bool>("PrintMode");

                    b.Property<decimal>("ZeroRange");

                    b.Property<decimal>("ZeroTrackWidth");

                    b.HasKey("ScaleSettingId");

                    b.ToTable("ScaleSettings");
                });

            modelBuilder.Entity("WeighrDAL.Models.Shift", b =>
                {
                    b.Property<int>("ShiftId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndTime");

                    b.Property<string>("Name");

                    b.Property<DateTime>("StartTime");

                    b.HasKey("ShiftId");

                    b.ToTable("Shifts");
                });

            modelBuilder.Entity("WeighrDAL.Models.ShiftTarget", b =>
                {
                    b.Property<int>("ShiftTargetId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("OrderId");

                    b.Property<string>("OrderNumber");

                    b.Property<int>("ProductId");

                    b.Property<DateTime>("ShiftDate");

                    b.Property<int>("ShiftId");

                    b.Property<int>("TargetUnits");

                    b.Property<int>("UnitsDone");

                    b.HasKey("ShiftTargetId");

                    b.ToTable("ShiftTargets");
                });

            modelBuilder.Entity("WeighrDAL.Models.TransactionLog", b =>
                {
                    b.Property<long>("TransactionLogId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("ActualWeight");

                    b.Property<string>("DeviceId");

                    b.Property<string>("OrderNumber");

                    b.Property<string>("ProductCode");

                    b.Property<int>("ShiftId");

                    b.Property<DateTime>("TransactionDate");

                    b.Property<decimal>("WeightDifference");

                    b.Property<int>("WeightStatus");

                    b.Property<string>("WeightType");

                    b.HasKey("TransactionLogId");

                    b.ToTable("TransactionLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
