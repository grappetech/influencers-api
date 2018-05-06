using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Action.Migrations
{
    public partial class MentionsSentimentAnalysis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "score",
                table: "SentimentKeyword",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "sadness",
                table: "EmotionsKeyword",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "joy",
                table: "EmotionsKeyword",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "fear",
                table: "EmotionsKeyword",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "disgust",
                table: "EmotionsKeyword",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "anger",
                table: "EmotionsKeyword",
                nullable: true,
                oldClrType: typeof(float),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "score",
                table: "SentimentKeyword",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "sadness",
                table: "EmotionsKeyword",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "joy",
                table: "EmotionsKeyword",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "fear",
                table: "EmotionsKeyword",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "disgust",
                table: "EmotionsKeyword",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "anger",
                table: "EmotionsKeyword",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
