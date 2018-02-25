using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Action.Migrations
{
    public partial class Briefing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "Briefings");

            migrationBuilder.RenameColumn(
                name: "Product",
                table: "Briefings",
                newName: "Value");

            migrationBuilder.AddColumn<string>(
                name: "AgeRange",
                table: "Briefings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Briefings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Entity",
                table: "Briefings",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EntityId",
                table: "Briefings",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Briefings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Briefings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Personality",
                table: "Briefings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Briefings",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Strength",
                table: "Briefings",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Tone",
                table: "Briefings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Briefings_EntityId",
                table: "Briefings",
                column: "EntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Briefings_Entities_EntityId",
                table: "Briefings",
                column: "EntityId",
                principalTable: "Entities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Briefings_Entities_EntityId",
                table: "Briefings");

            migrationBuilder.DropIndex(
                name: "IX_Briefings_EntityId",
                table: "Briefings");

            migrationBuilder.DropColumn(
                name: "AgeRange",
                table: "Briefings");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Briefings");

            migrationBuilder.DropColumn(
                name: "Entity",
                table: "Briefings");

            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "Briefings");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Briefings");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Briefings");

            migrationBuilder.DropColumn(
                name: "Personality",
                table: "Briefings");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Briefings");

            migrationBuilder.DropColumn(
                name: "Strength",
                table: "Briefings");

            migrationBuilder.DropColumn(
                name: "Tone",
                table: "Briefings");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Briefings",
                newName: "Product");

            migrationBuilder.AddColumn<byte[]>(
                name: "File",
                table: "Briefings",
                nullable: true);
        }
    }
}
