using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Action.Migrations
{
    public partial class AddColumnIndustriID_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IndustryId",
                table: "Entities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entities_IndustryId",
                table: "Entities",
                column: "IndustryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entities_Industries_IndustryId",
                table: "Entities",
                column: "IndustryId",
                principalTable: "Industries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entities_Industries_IndustryId",
                table: "Entities");

            migrationBuilder.DropIndex(
                name: "IX_Entities_IndustryId",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "IndustryId",
                table: "Entities");
        }
    }
}
