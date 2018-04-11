using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WeighrDAL.Migrations
{
    public partial class Mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<decimal>(
                name: "Resolution",
                table: "ScaleConfigs",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TargetWeight",
                table: "Products",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaximumCapacity",
                table: "ScaleConfigs");

            migrationBuilder.DropColumn(
                name: "MinimumDivision",
                table: "ScaleConfigs");

            migrationBuilder.DropColumn(
                name: "Resolution",
                table: "ScaleConfigs");

            migrationBuilder.DropColumn(
                name: "TargetWeight",
                table: "Products");
        }
    }
}
