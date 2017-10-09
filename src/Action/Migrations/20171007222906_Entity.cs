using Microsoft.EntityFrameworkCore.Migrations;

namespace Action.Migrations
{
    public partial class Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Category",
                "Entities");

            migrationBuilder.AddColumn<int>(
                "CategoryId",
                "Entities",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "CategoryId",
                "Entities");

            migrationBuilder.AddColumn<string>(
                "Category",
                "Entities",
                nullable: true);
        }
    }
}