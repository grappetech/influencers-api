using Microsoft.EntityFrameworkCore.Migrations;

namespace Action.Migrations
{
    public partial class PageStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                "PageStatus",
                "ScrapSources",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "PageStatus",
                "ScrapSources");
        }
    }
}