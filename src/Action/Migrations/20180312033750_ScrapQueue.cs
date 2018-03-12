using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Action.Migrations
{
    public partial class ScrapQueue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScrapQueue",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Completed = table.Column<bool>(nullable: false),
                    EndDateTime = table.Column<DateTime>(nullable: true),
                    EnqueueDateTime = table.Column<DateTime>(nullable: false),
                    StartDateTime = table.Column<DateTime>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScrapQueue", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScrapQueue");
        }
    }
}
