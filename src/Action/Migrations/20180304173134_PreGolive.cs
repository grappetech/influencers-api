using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Action.Migrations
{
    public partial class PreGolive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConnectedEntityId",
                table: "Briefings",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ConnectedEntityId1",
                table: "Briefings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Report",
                table: "Briefings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Briefings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Briefings_ConnectedEntityId1",
                table: "Briefings",
                column: "ConnectedEntityId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Briefings_Entities_ConnectedEntityId1",
                table: "Briefings",
                column: "ConnectedEntityId1",
                principalTable: "Entities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Briefings_Entities_ConnectedEntityId1",
                table: "Briefings");

            migrationBuilder.DropIndex(
                name: "IX_Briefings_ConnectedEntityId1",
                table: "Briefings");

            migrationBuilder.DropColumn(
                name: "ConnectedEntityId",
                table: "Briefings");

            migrationBuilder.DropColumn(
                name: "ConnectedEntityId1",
                table: "Briefings");

            migrationBuilder.DropColumn(
                name: "Report",
                table: "Briefings");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Briefings");
        }
    }
}
