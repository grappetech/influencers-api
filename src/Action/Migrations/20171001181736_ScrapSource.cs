using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Action.Migrations
{
    public partial class ScrapSource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "ScrapSources",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Alias = table.Column<string>(nullable: true),
                    Dept = table.Column<int>(nullable: false),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_ScrapSources", x => x.Id); });

            migrationBuilder.CreateTable(
                "ScrapedPages",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Date = table.Column<DateTime>(nullable: false),
                    ScrapSourceId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScrapedPages", x => x.Id);
                    table.ForeignKey(
                        "FK_ScrapedPages_ScrapSources_ScrapSourceId",
                        x => x.ScrapSourceId,
                        "ScrapSources",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_ScrapedPages_ScrapSourceId",
                "ScrapedPages",
                "ScrapSourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "ScrapedPages");

            migrationBuilder.DropTable(
                "ScrapSources");
        }
    }
}