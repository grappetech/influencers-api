using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Action.Migrations
{
    public partial class EntityProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FacebookUser",
                table: "Entities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstagranUser",
                table: "Entities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "Entities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SiteUrl",
                table: "Entities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TweeterUser",
                table: "Entities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YoutubeUser",
                table: "Entities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacebookUser",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "InstagranUser",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "SiteUrl",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "TweeterUser",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "YoutubeUser",
                table: "Entities");
        }
    }
}
