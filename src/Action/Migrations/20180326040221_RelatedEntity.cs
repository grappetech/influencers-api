using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Action.Migrations
{
    public partial class RelatedEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "Tones");

            migrationBuilder.AddColumn<Guid>(
                name: "NluEntityId",
                table: "Tones",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<long>(
                name: "EntityId",
                table: "Entity",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entity_EntityId",
                table: "Entity",
                column: "EntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entity_Entities_EntityId",
                table: "Entity",
                column: "EntityId",
                principalTable: "Entities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entity_Entities_EntityId",
                table: "Entity");

            migrationBuilder.DropIndex(
                name: "IX_Entity_EntityId",
                table: "Entity");

            migrationBuilder.DropColumn(
                name: "NluEntityId",
                table: "Tones");

            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "Entity");

            migrationBuilder.AddColumn<long>(
                name: "EntityId",
                table: "Tones",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
