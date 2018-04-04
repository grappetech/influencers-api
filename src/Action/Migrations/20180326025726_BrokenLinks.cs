using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Action.Migrations
{
    public partial class BrokenLinks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScrapSourceId",
                table: "Tones");

            migrationBuilder.CreateTable(
                name: "BrickedSources",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Step = table.Column<string>(maxLength: 200, nullable: true),
                    Title = table.Column<string>(maxLength: 400, nullable: true),
                    Url = table.Column<string>(maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrickedSources", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrickedSources");

            migrationBuilder.AddColumn<int>(
                name: "ScrapSourceId",
                table: "Tones",
                nullable: false,
                defaultValue: 0);
        }
    }
}
