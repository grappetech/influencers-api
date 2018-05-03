using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Action.Migrations
{
    public partial class KewordsFragments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "fragment",
                table: "Keywords",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "translatedFragment",
                table: "Keywords",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fragment",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "translatedFragment",
                table: "Keywords");
        }
    }
}
