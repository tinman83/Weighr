using Microsoft.EntityFrameworkCore.Migrations;
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
                name: "Batches",
                columns: table => new
                {
                    BatchId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BatchCode = table.Column<string>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    isCurrent = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batches", x => x.BatchId);
                });

            migrationBuilder.CreateTable(
                name: "DeviceInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientId = table.Column<string>(nullable: true),
                    MachineName = table.Column<string>(nullable: true),
                    Manufacturer = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    OSName = table.Column<string>(nullable: true),
                    PlantId = table.Column<string>(nullable: true),
                    SerialNumber = table.Column<string>(nullable: true),
                    ServerUrl = table.Column<string>(nullable: true),
                    iotHubDeviceKey = table.Column<string>(nullable: true),
                    iotHubUri = table.Column<string>(nullable: true),
                    pushToCloud = table.Column<bool>(nullable: false),
                    pushToWebApi = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceInfos", x => x.Id);
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
                    DribblePoint = table.Column<decimal>(nullable: false),
                    ExpectedFillTime = table.Column<decimal>(nullable: false),
                    Inflight = table.Column<decimal>(nullable: false),
                    LowerLimit = table.Column<decimal>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ProductCode = table.Column<string>(nullable: true),
                    TargetWeight = table.Column<decimal>(nullable: false),
                    UpperLimit = table.Column<decimal>(nullable: false),
                    isCurrent = table.Column<bool>(nullable: false),
                    rowguid = table.Column<Guid>(nullable: false)
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
                    Gradient = table.Column<decimal>(nullable: false),
                    Resolution = table.Column<decimal>(nullable: false),
                    YIntercept = table.Column<decimal>(nullable: false),
                    offset = table.Column<decimal>(nullable: false)
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
                    Density = table.Column<decimal>(nullable: false),
                    DisplayUnits = table.Column<string>(nullable: true),
                    DisplayUnitsWeight = table.Column<decimal>(nullable: false),
                    Inflight = table.Column<decimal>(nullable: false),
                    InflightTiming = table.Column<int>(nullable: false),
                    LowerLimit = table.Column<decimal>(nullable: false),
                    MaximumCapacity = table.Column<decimal>(nullable: false),
                    MinimumDivision = table.Column<decimal>(nullable: false),
                    PrintMode = table.Column<bool>(nullable: false),
                    UpperLimit = table.Column<decimal>(nullable: false),
                    ZeroRange = table.Column<decimal>(nullable: false)
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
                    ActualFillTime = table.Column<decimal>(nullable: false),
                    ActualWeight = table.Column<decimal>(nullable: false),
                    BaseUnitOfMeasure = table.Column<string>(nullable: true),
                    BatchCode = table.Column<string>(nullable: true),
                    ClientId = table.Column<string>(nullable: true),
                    DateUploaded = table.Column<DateTime>(nullable: false),
                    DeviceId = table.Column<string>(nullable: true),
                    ExpectedFillTime = table.Column<decimal>(nullable: false),
                    MachineName = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    OrderNumber = table.Column<string>(nullable: true),
                    PercDiffFillTime = table.Column<decimal>(nullable: false),
                    PlantId = table.Column<string>(nullable: true),
                    ProductCode = table.Column<string>(nullable: true),
                    ProductDensity = table.Column<decimal>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    SerialNumber = table.Column<string>(nullable: true),
                    ShiftId = table.Column<int>(nullable: false),
                    TargetWeight = table.Column<decimal>(nullable: false),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    Units = table.Column<string>(nullable: true),
                    Uploaded = table.Column<bool>(nullable: false),
                    WeightDifference = table.Column<decimal>(nullable: false),
                    WeightStatus = table.Column<int>(nullable: false),
                    WeightType = table.Column<string>(nullable: true),
                    persistedServer = table.Column<bool>(nullable: false),
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
                name: "Batches");

            migrationBuilder.DropTable(
                name: "DeviceInfos");

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
