using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Action.Migrations
{
    public partial class Add_ScrapSourceEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScrapSourceEntity",
                columns: table => new
                {
                    ScrapSourceId = table.Column<int>(nullable: false),
                    EntityId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScrapSourceEntity", x => new { x.ScrapSourceId, x.EntityId });
                    table.ForeignKey(
                        name: "FK_ScrapSourceEntity_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScrapSourceEntity_ScrapSources_ScrapSourceId",
                        column: x => x.ScrapSourceId,
                        principalTable: "ScrapSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScrapSourceEntity_EntityId",
                table: "ScrapSourceEntity",
                column: "EntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScrapSourceEntity");
        }
    }
}
