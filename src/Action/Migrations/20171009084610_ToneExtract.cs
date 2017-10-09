using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Action.Migrations
{
    public partial class ToneExtract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personalities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    EntityId = table.Column<long>(nullable: false),
                    ScrapSourceId = table.Column<int>(nullable: false),
                    ScrapedPageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTone",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTone", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConsumptionPreferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    ConsumptionPreferenceId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PersonalityResultId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumptionPreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsumptionPreferences_Personalities_PersonalityResultId",
                        column: x => x.PersonalityResultId,
                        principalTable: "Personalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Personality",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Name = table.Column<string>(nullable: true),
                    Percentile = table.Column<double>(nullable: true),
                    PersonalityResultId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personality", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personality_Personalities_PersonalityResultId",
                        column: x => x.PersonalityResultId,
                        principalTable: "Personalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tones",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    DocumentToneId = table.Column<Guid>(nullable: true),
                    EntityId = table.Column<long>(nullable: false),
                    ScrapSourceId = table.Column<int>(nullable: false),
                    ScrapedPageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tones_DocumentTone_DocumentToneId",
                        column: x => x.DocumentToneId,
                        principalTable: "DocumentTone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConsumptionDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    ConsumptionPreferencesId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumptionDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsumptionDetail_ConsumptionPreferences_ConsumptionPreferencesId",
                        column: x => x.ConsumptionPreferencesId,
                        principalTable: "ConsumptionPreferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Detail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Name = table.Column<string>(nullable: true),
                    Percentile = table.Column<double>(nullable: true),
                    PersonalityId = table.Column<Guid>(nullable: true),
                    PersonalityResultId = table.Column<Guid>(nullable: true),
                    PersonalityResultId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Detail_Personality_PersonalityId",
                        column: x => x.PersonalityId,
                        principalTable: "Personality",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detail_Personalities_PersonalityResultId",
                        column: x => x.PersonalityResultId,
                        principalTable: "Personalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detail_Personalities_PersonalityResultId1",
                        column: x => x.PersonalityResultId1,
                        principalTable: "Personalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SentencesTone",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    InputFrom = table.Column<long>(nullable: true),
                    InputTo = table.Column<long>(nullable: true),
                    SentenceId = table.Column<long>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    ToneResultId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentencesTone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SentencesTone_Tones_ToneResultId",
                        column: x => x.ToneResultId,
                        principalTable: "Tones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ToneCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    CategoryId = table.Column<string>(nullable: true),
                    CategoryName = table.Column<string>(nullable: true),
                    DocumentToneId = table.Column<Guid>(nullable: true),
                    SentencesToneId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToneCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToneCategories_DocumentTone_DocumentToneId",
                        column: x => x.DocumentToneId,
                        principalTable: "DocumentTone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ToneCategories_SentencesTone_SentencesToneId",
                        column: x => x.SentencesToneId,
                        principalTable: "SentencesTone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tone",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Score = table.Column<double>(nullable: true),
                    ToneCategoriesId = table.Column<Guid>(nullable: true),
                    ToneId = table.Column<string>(nullable: true),
                    ToneName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tone_ToneCategories_ToneCategoriesId",
                        column: x => x.ToneCategoriesId,
                        principalTable: "ToneCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsumptionDetail_ConsumptionPreferencesId",
                table: "ConsumptionDetail",
                column: "ConsumptionPreferencesId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumptionPreferences_PersonalityResultId",
                table: "ConsumptionPreferences",
                column: "PersonalityResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Detail_PersonalityId",
                table: "Detail",
                column: "PersonalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Detail_PersonalityResultId",
                table: "Detail",
                column: "PersonalityResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Detail_PersonalityResultId1",
                table: "Detail",
                column: "PersonalityResultId1");

            migrationBuilder.CreateIndex(
                name: "IX_Personality_PersonalityResultId",
                table: "Personality",
                column: "PersonalityResultId");

            migrationBuilder.CreateIndex(
                name: "IX_SentencesTone_ToneResultId",
                table: "SentencesTone",
                column: "ToneResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Tone_ToneCategoriesId",
                table: "Tone",
                column: "ToneCategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_ToneCategories_DocumentToneId",
                table: "ToneCategories",
                column: "DocumentToneId");

            migrationBuilder.CreateIndex(
                name: "IX_ToneCategories_SentencesToneId",
                table: "ToneCategories",
                column: "SentencesToneId");

            migrationBuilder.CreateIndex(
                name: "IX_Tones_DocumentToneId",
                table: "Tones",
                column: "DocumentToneId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsumptionDetail");

            migrationBuilder.DropTable(
                name: "Detail");

            migrationBuilder.DropTable(
                name: "Tone");

            migrationBuilder.DropTable(
                name: "ConsumptionPreferences");

            migrationBuilder.DropTable(
                name: "Personality");

            migrationBuilder.DropTable(
                name: "ToneCategories");

            migrationBuilder.DropTable(
                name: "Personalities");

            migrationBuilder.DropTable(
                name: "SentencesTone");

            migrationBuilder.DropTable(
                name: "Tones");

            migrationBuilder.DropTable(
                name: "DocumentTone");
        }
    }
}
