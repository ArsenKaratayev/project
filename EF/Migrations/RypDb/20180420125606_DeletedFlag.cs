using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Project.Migrations.RypDb
{
    public partial class DeletedFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Deleted",
                table: "Subjects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Deleted",
                table: "Specialties",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Deleted",
                table: "Ryps",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Prototype",
                table: "Ryps",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Deleted",
                table: "ElectiveGroups",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Specialties");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Ryps");

            migrationBuilder.DropColumn(
                name: "Prototype",
                table: "Ryps");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "ElectiveGroups");
        }
    }
}
