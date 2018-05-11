using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WeighrDAL.Migrations
{
    public partial class Mig6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PowerOnZero",
                table: "ScaleSettings");

            migrationBuilder.DropColumn(
                name: "ZeroTrackWidth",
                table: "ScaleSettings");

            migrationBuilder.DropColumn(
                name: "MaximumCapacity",
                table: "ScaleConfigs");

            migrationBuilder.DropColumn(
                name: "MinimumDivision",
                table: "ScaleConfigs");

            migrationBuilder.RenameColumn(
                name: "DecimalPointPosition",
                table: "ScaleSettings",
                newName: "DecimalPointPrecision");

            migrationBuilder.AddColumn<string>(
                name: "Units",
                table: "TransactionLogs",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "ZeroRange",
                table: "ScaleSettings",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<double>(
                name: "Density",
                table: "ScaleSettings",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DisplayUnitsWeight",
                table: "ScaleSettings",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MaximumCapacity",
                table: "ScaleSettings",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MinimumDivision",
                table: "ScaleSettings",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<double>(
                name: "offset",
                table: "ScaleConfigs",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<double>(
                name: "Inflight",
                table: "Products",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Units",
                table: "TransactionLogs");

            migrationBuilder.DropColumn(
                name: "Density",
                table: "ScaleSettings");

            migrationBuilder.DropColumn(
                name: "DisplayUnitsWeight",
                table: "ScaleSettings");

            migrationBuilder.DropColumn(
                name: "MaximumCapacity",
                table: "ScaleSettings");

            migrationBuilder.DropColumn(
                name: "MinimumDivision",
                table: "ScaleSettings");

            migrationBuilder.DropColumn(
                name: "Inflight",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "DecimalPointPrecision",
                table: "ScaleSettings",
                newName: "DecimalPointPosition");

            migrationBuilder.AlterColumn<decimal>(
                name: "ZeroRange",
                table: "ScaleSettings",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<bool>(
                name: "PowerOnZero",
                table: "ScaleSettings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "ZeroTrackWidth",
                table: "ScaleSettings",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "offset",
                table: "ScaleConfigs",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<decimal>(
                name: "MaximumCapacity",
                table: "ScaleConfigs",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumDivision",
                table: "ScaleConfigs",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
