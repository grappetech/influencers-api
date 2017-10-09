using Microsoft.EntityFrameworkCore.Migrations;

namespace Action.Migrations
{
    public partial class ScrapSourceTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "EndTag",
                "ScrapSources",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "Limit",
                "ScrapSources",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                "StarTag",
                "ScrapSources",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "EndTag",
                "ScrapSources");

            migrationBuilder.DropColumn(
                "Limit",
                "ScrapSources");

            migrationBuilder.DropColumn(
                "StarTag",
                "ScrapSources");
        }
    }
}