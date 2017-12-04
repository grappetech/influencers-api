using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Action.Migrations
{
    public partial class Photo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Entities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entities_AccountId",
                table: "Entities",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entities_Accounts_AccountId",
                table: "Entities",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entities_Accounts_AccountId",
                table: "Entities");

            migrationBuilder.DropIndex(
                name: "IX_Entities_AccountId",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Entities");
        }
    }
}
