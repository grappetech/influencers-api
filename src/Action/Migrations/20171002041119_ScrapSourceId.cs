using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Action.Migrations
{
    public partial class ScrapSourceId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScrapedPages_ScrapSources_ScrapSourceId",
                table: "ScrapedPages");

            migrationBuilder.AlterColumn<int>(
                name: "ScrapSourceId",
                table: "ScrapedPages",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ScrapedPages_ScrapSources_ScrapSourceId",
                table: "ScrapedPages",
                column: "ScrapSourceId",
                principalTable: "ScrapSources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScrapedPages_ScrapSources_ScrapSourceId",
                table: "ScrapedPages");

            migrationBuilder.AlterColumn<int>(
                name: "ScrapSourceId",
                table: "ScrapedPages",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ScrapedPages_ScrapSources_ScrapSourceId",
                table: "ScrapedPages",
                column: "ScrapSourceId",
                principalTable: "ScrapSources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
