using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WeighrDAL.Migrations
{
    public partial class Mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccumulatedWeights",
                columns: table => new
                {
                    AccumulateWeightId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CurrentDate = table.Column<DateTime>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Weight = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccumulatedWeights", x => x.AccumulateWeightId);
                });

            migrationBuilder.CreateTable(
                name: "ScaleSettings",
                columns: table => new
                {
                    ScaleSettingId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DecimalPointPosition = table.Column<int>(nullable: false),
                    DecimalPointPositionName = table.Column<string>(nullable: true),
                    DisplayUnits = table.Column<string>(nullable: true),
                    PowerOnZero = table.Column<bool>(nullable: false),
                    PrintMode = table.Column<bool>(nullable: false),
                    ZeroRange = table.Column<decimal>(nullable: false),
                    ZeroTrackWidth = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScaleSettings", x => x.ScaleSettingId);
                });

            migrationBuilder.CreateTable(
                name: "TransactionLogs",
                columns: table => new
                {
                    TransactionLogId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ActualWeight = table.Column<decimal>(nullable: false),
                    DeviceId = table.Column<string>(nullable: true),
                    OrderNumber = table.Column<string>(nullable: true),
                    ProductCode = table.Column<string>(nullable: true),
                    ShiftId = table.Column<int>(nullable: false),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    WeightDifference = table.Column<decimal>(nullable: false),
                    WeightStatus = table.Column<int>(nullable: false),
                    WeightType = table.Column<string>(nullable: true)
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
                name: "ScaleSettings");

            migrationBuilder.DropTable(
                name: "TransactionLogs");
        }
    }
}
