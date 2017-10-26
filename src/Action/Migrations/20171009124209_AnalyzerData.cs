using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Action.Migrations
{
    public partial class AnalyzerData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Briefings",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Analysis = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Factor = table.Column<string>(nullable: true),
                    File = table.Column<byte[]>(nullable: true),
                    Product = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Briefings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Briefings");
        }
    }
}
