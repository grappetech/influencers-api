using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Action.Migrations
{
    public partial class ScrapSourceTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EndTag",
                table: "ScrapSources",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Limit",
                table: "ScrapSources",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StarTag",
                table: "ScrapSources",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTag",
                table: "ScrapSources");

            migrationBuilder.DropColumn(
                name: "Limit",
                table: "ScrapSources");

            migrationBuilder.DropColumn(
                name: "StarTag",
                table: "ScrapSources");
        }
    }
}
