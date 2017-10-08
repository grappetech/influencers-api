using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Action.Migrations
{
    public partial class Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Entities");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Entities",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Entities");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Entities",
                nullable: true);
        }
    }
}
