using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Action.Migrations
{
    public partial class Add_ScrapSourceIndustries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScrapSourceIndustry",
                columns: table => new
                {
                    ScrapSourceId = table.Column<int>(nullable: false),
                    IndustryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScrapSourceIndustry", x => new { x.ScrapSourceId, x.IndustryId });
                    table.ForeignKey(
                        name: "FK_ScrapSourceIndustry_Industries_IndustryId",
                        column: x => x.IndustryId,
                        principalTable: "Industries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScrapSourceIndustry_ScrapSources_ScrapSourceId",
                        column: x => x.ScrapSourceId,
                        principalTable: "ScrapSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScrapSourceIndustry_IndustryId",
                table: "ScrapSourceIndustry",
                column: "IndustryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScrapSourceIndustry");
        }
    }
}
