using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Action.Migrations
{
    public partial class AlterWatonEntity_AddBirthDate_Genre_Ethnicity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Entities",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ethnicity",
                table: "Entities",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Entities",
                type: "char(1)",
                maxLength: 1,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "Ethnicity",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Entities");
        }
    }
}
