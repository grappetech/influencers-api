using Microsoft.EntityFrameworkCore.Migrations;

namespace Action.Migrations
{
    public partial class ScrapSourceId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_ScrapedPages_ScrapSources_ScrapSourceId",
                "ScrapedPages");

            migrationBuilder.AlterColumn<int>(
                "ScrapSourceId",
                "ScrapedPages",
                nullable: true);

            migrationBuilder.AddForeignKey(
                "FK_ScrapedPages_ScrapSources_ScrapSourceId",
                "ScrapedPages",
                "ScrapSourceId",
                "ScrapSources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_ScrapedPages_ScrapSources_ScrapSourceId",
                "ScrapedPages");

            migrationBuilder.AlterColumn<int>(
                "ScrapSourceId",
                "ScrapedPages",
                nullable: false);

            migrationBuilder.AddForeignKey(
                "FK_ScrapedPages_ScrapSources_ScrapSourceId",
                "ScrapedPages",
                "ScrapSourceId",
                "ScrapSources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}