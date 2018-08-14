using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Action.Migrations
{
    public partial class BriefingCorrectEntityId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ConnectedEntityId",
                table: "Briefings",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Briefings_ConnectedEntityId",
                table: "Briefings",
                column: "ConnectedEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Briefings_Entities_ConnectedEntityId",
                table: "Briefings",
                column: "ConnectedEntityId",
                principalTable: "Entities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Briefings_Entities_ConnectedEntityId",
                table: "Briefings");

            migrationBuilder.DropIndex(
                name: "IX_Briefings_ConnectedEntityId",
                table: "Briefings");

            migrationBuilder.AlterColumn<int>(
                name: "ConnectedEntityId",
                table: "Briefings",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
