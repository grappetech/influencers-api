using Microsoft.EntityFrameworkCore.Migrations;

namespace Action.Migrations
{
    public partial class PageProccessStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                "Status",
                "ScrapedPages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                "Translated",
                "ScrapedPages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Status",
                "ScrapedPages");

            migrationBuilder.DropColumn(
                "Translated",
                "ScrapedPages");
        }
    }
}