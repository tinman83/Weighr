﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WeighrDAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccumulatedWeights",
                columns: table => new
                {
                    AccumulatedWeightId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CurrentDate = table.Column<DateTime>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Weight = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccumulatedWeights", x => x.AccumulatedWeightId);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Units = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DueDate = table.Column<DateTime>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    OrderNumber = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Density = table.Column<decimal>(nullable: false),
                    Inflight = table.Column<double>(nullable: false),
                    LowerLimit = table.Column<decimal>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ProductCode = table.Column<string>(nullable: true),
                    TargetWeight = table.Column<decimal>(nullable: false),
                    UpperLimit = table.Column<decimal>(nullable: false),
                    isCurrent = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "ScaleConfigs",
                columns: table => new
                {
                    ScaleConfigId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Gradient = table.Column<double>(nullable: false),
                    Resolution = table.Column<decimal>(nullable: false),
                    YIntercept = table.Column<double>(nullable: false),
                    offset = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScaleConfigs", x => x.ScaleConfigId);
                });

            migrationBuilder.CreateTable(
                name: "ScaleSettings",
                columns: table => new
                {
                    ScaleSettingId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DecimalPointPositionName = table.Column<string>(nullable: true),
                    DecimalPointPrecision = table.Column<int>(nullable: false),
                    Density = table.Column<double>(nullable: false),
                    DisplayUnits = table.Column<string>(nullable: true),
                    DisplayUnitsWeight = table.Column<double>(nullable: false),
                    MaximumCapacity = table.Column<double>(nullable: false),
                    MinimumDivision = table.Column<double>(nullable: false),
                    PrintMode = table.Column<bool>(nullable: false),
                    ZeroRange = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScaleSettings", x => x.ScaleSettingId);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    ShiftId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EndTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.ShiftId);
                });

            migrationBuilder.CreateTable(
                name: "ShiftTargets",
                columns: table => new
                {
                    ShiftTargetId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(nullable: false),
                    OrderNumber = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    ShiftDate = table.Column<DateTime>(nullable: false),
                    ShiftId = table.Column<int>(nullable: false),
                    TargetUnits = table.Column<int>(nullable: false),
                    UnitsDone = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftTargets", x => x.ShiftTargetId);
                });

            migrationBuilder.CreateTable(
                name: "TransactionLogs",
                columns: table => new
                {
                    TransactionLogId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ActualWeight = table.Column<decimal>(nullable: false),
                    DateUploaded = table.Column<DateTime>(nullable: false),
                    DeviceId = table.Column<string>(nullable: true),
                    OrderNumber = table.Column<string>(nullable: true),
                    ProductCode = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    ShiftId = table.Column<int>(nullable: false),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    Units = table.Column<string>(nullable: true),
                    Uploaded = table.Column<bool>(nullable: false),
                    WeightDifference = table.Column<decimal>(nullable: false),
                    WeightStatus = table.Column<int>(nullable: false),
                    WeightType = table.Column<string>(nullable: true),
                    rowguid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionLogs", x => x.TransactionLogId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccumulatedWeights");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ScaleConfigs");

            migrationBuilder.DropTable(
                name: "ScaleSettings");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "ShiftTargets");

            migrationBuilder.DropTable(
                name: "TransactionLogs");
        }
    }
}
