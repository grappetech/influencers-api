using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Action.Migrations
{
    public partial class Start : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Disambiguation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Dbpedia_resource = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disambiguation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTone",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTone", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmotionDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Anger = table.Column<float>(nullable: true),
                    Disgust = table.Column<float>(nullable: true),
                    Fear = table.Column<float>(nullable: true),
                    Joy = table.Column<float>(nullable: true),
                    Sadness = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmotionDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmotionsKeyword",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    anger = table.Column<float>(nullable: true),
                    disgust = table.Column<float>(nullable: true),
                    fear = table.Column<float>(nullable: true),
                    joy = table.Column<float>(nullable: true),
                    sadness = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmotionsKeyword", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityMentions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    EntityId = table.Column<long>(nullable: false),
                    ScrapSourceId = table.Column<int>(nullable: false),
                    ScrapedPageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityMentions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Base64Image = table.Column<string>(maxLength: 2147483647, nullable: true),
                    ImageName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Industries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Industries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Metadata",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    image = table.Column<string>(nullable: true),
                    language = table.Column<string>(nullable: true),
                    publication_date = table.Column<DateTime>(nullable: false),
                    retrieved_url = table.Column<string>(nullable: true),
                    title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metadata", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permitions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ActionName = table.Column<string>(nullable: true),
                    ActionPermition = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personalities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EntityId = table.Column<long>(nullable: false),
                    ScrapSourceId = table.Column<int>(nullable: false),
                    ScrapedPageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Slug = table.Column<string>(nullable: true),
                    TypePlan = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelationTypes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScrapQueue",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Completed = table.Column<bool>(nullable: false),
                    EndDateTime = table.Column<DateTime>(nullable: true),
                    EnqueueDateTime = table.Column<DateTime>(nullable: false),
                    StartDateTime = table.Column<DateTime>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScrapQueue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScrapSources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Alias = table.Column<string>(nullable: true),
                    Dept = table.Column<int>(nullable: false),
                    EndTag = table.Column<string>(nullable: true),
                    Limit = table.Column<int>(nullable: false),
                    PageStatus = table.Column<int>(nullable: false),
                    StarTag = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScrapSources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SemanticObject",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemanticObject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SemanticSubject",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemanticSubject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SemanticVerb",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Tense = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemanticVerb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SentimentDocument",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Label = table.Column<string>(nullable: true),
                    Score = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentimentDocument", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SentimentKeyword",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    score = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentimentKeyword", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Visitors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 2, nullable: true),
                    CountryId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 180, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                    table.ForeignKey(
                        name: "FK_States_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DisambiguationSubtype",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DisambiguationId = table.Column<Guid>(nullable: true),
                    SubType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisambiguationSubtype", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisambiguationSubtype_Disambiguation_DisambiguationId",
                        column: x => x.DisambiguationId,
                        principalTable: "Disambiguation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tones",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
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
                name: "EmotionDocument",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmotionDetailId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmotionDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmotionDocument_EmotionDetail_EmotionDetailId",
                        column: x => x.EmotionDetailId,
                        principalTable: "EmotionDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MetadataId = table.Column<Guid>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Author_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Feed",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MetadataId = table.Column<Guid>(nullable: true),
                    link = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feed_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConsumptionPreferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
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
                    Id = table.Column<Guid>(nullable: false),
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
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    PlanId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Features_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScrapedPages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Hash = table.Column<string>(nullable: true),
                    ScrapSourceId = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    Translated = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScrapedPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScrapedPages_ScrapSources_ScrapSourceId",
                        column: x => x.ScrapSourceId,
                        principalTable: "ScrapSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SemanticAction",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Normalized = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    VerbId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemanticAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SemanticAction_SemanticVerb_VerbId",
                        column: x => x.VerbId,
                        principalTable: "SemanticVerb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sentiment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RetrievedUrl = table.Column<string>(nullable: true),
                    SentimentDocId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sentiment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sentiment_SentimentDocument_SentimentDocId",
                        column: x => x.SentimentDocId,
                        principalTable: "SentimentDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    StateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SentencesTone",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
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
                name: "Emotion",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmotionDocumentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emotion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emotion_EmotionDocument_EmotionDocumentId",
                        column: x => x.EmotionDocumentId,
                        principalTable: "EmotionDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConsumptionDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
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
                    Id = table.Column<Guid>(nullable: false),
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
                name: "SentimentTarget",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Label = table.Column<string>(nullable: true),
                    Score = table.Column<float>(nullable: true),
                    SentimentId = table.Column<Guid>(nullable: true),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentimentTarget", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SentimentTarget_Sentiment_SentimentId",
                        column: x => x.SentimentId,
                        principalTable: "Sentiment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ToneCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
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
                name: "EmotionTarget",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmotionDetailId = table.Column<Guid>(nullable: true),
                    EmotionId = table.Column<Guid>(nullable: true),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmotionTarget", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmotionTarget_EmotionDetail_EmotionDetailId",
                        column: x => x.EmotionDetailId,
                        principalTable: "EmotionDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmotionTarget_Emotion_EmotionId",
                        column: x => x.EmotionId,
                        principalTable: "Emotion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NluResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmotionId = table.Column<Guid>(nullable: true),
                    MetadataId = table.Column<Guid>(nullable: true),
                    ScrapedPageId = table.Column<Guid>(nullable: false),
                    SentimentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NluResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NluResults_Emotion_EmotionId",
                        column: x => x.EmotionId,
                        principalTable: "Emotion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NluResults_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalTable: "Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NluResults_Sentiment_SentimentId",
                        column: x => x.SentimentId,
                        principalTable: "Sentiment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tone",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Label = table.Column<string>(nullable: true),
                    NLUResultId = table.Column<Guid>(nullable: true),
                    Score = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_NluResults_NLUResultId",
                        column: x => x.NLUResultId,
                        principalTable: "NluResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Concept",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Dbpedia_resource = table.Column<string>(nullable: true),
                    NLUResultId = table.Column<Guid>(nullable: true),
                    Relevance = table.Column<float>(nullable: true),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concept", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Concept_NluResults_NLUResultId",
                        column: x => x.NLUResultId,
                        principalTable: "NluResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Entity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Count = table.Column<long>(nullable: true),
                    DisambiguationId = table.Column<Guid>(nullable: true),
                    NLUResultId = table.Column<Guid>(nullable: true),
                    Relevance = table.Column<float>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entity_Disambiguation_DisambiguationId",
                        column: x => x.DisambiguationId,
                        principalTable: "Disambiguation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entity_NluResults_NLUResultId",
                        column: x => x.NLUResultId,
                        principalTable: "NluResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Keywords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NLUResultId = table.Column<Guid>(nullable: true),
                    emotionsId = table.Column<Guid>(nullable: true),
                    language = table.Column<string>(nullable: true),
                    relevance = table.Column<float>(nullable: true),
                    retrieved_url = table.Column<string>(nullable: true),
                    sentimentId = table.Column<Guid>(nullable: true),
                    text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keywords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Keywords_NluResults_NLUResultId",
                        column: x => x.NLUResultId,
                        principalTable: "NluResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Keywords_EmotionsKeyword_emotionsId",
                        column: x => x.emotionsId,
                        principalTable: "EmotionsKeyword",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Keywords_SentimentKeyword_sentimentId",
                        column: x => x.sentimentId,
                        principalTable: "SentimentKeyword",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Relation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NLUResultId = table.Column<Guid>(nullable: true),
                    language = table.Column<string>(nullable: true),
                    score = table.Column<float>(nullable: true),
                    sentence = table.Column<string>(nullable: true),
                    type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relation_NluResults_NLUResultId",
                        column: x => x.NLUResultId,
                        principalTable: "NluResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SemanticRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ActionId = table.Column<Guid>(nullable: true),
                    NLUResultId = table.Column<Guid>(nullable: true),
                    ObjectId = table.Column<Guid>(nullable: true),
                    Sentence = table.Column<string>(nullable: true),
                    SubjectId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemanticRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SemanticRole_SemanticAction_ActionId",
                        column: x => x.ActionId,
                        principalTable: "SemanticAction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SemanticRole_NluResults_NLUResultId",
                        column: x => x.NLUResultId,
                        principalTable: "NluResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SemanticRole_SemanticObject_ObjectId",
                        column: x => x.ObjectId,
                        principalTable: "SemanticObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SemanticRole_SemanticSubject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "SemanticSubject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Argument",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RelationId = table.Column<Guid>(nullable: true),
                    text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Argument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Argument_Relation_RelationId",
                        column: x => x.RelationId,
                        principalTable: "Relation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntityRelation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ArgumentId = table.Column<Guid>(nullable: true),
                    text = table.Column<string>(nullable: true),
                    type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityRelation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityRelation_Argument_ArgumentId",
                        column: x => x.ArgumentId,
                        principalTable: "Argument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    AccountId = table.Column<int>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    PlanId = table.Column<int>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ActivationDate = table.Column<DateTime>(nullable: false),
                    AdministratorId = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PlanId = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_AspNetUsers_AdministratorId",
                        column: x => x.AdministratorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: true),
                    Alias = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    FacebookUser = table.Column<string>(nullable: true),
                    InstagranUser = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PictureUrl = table.Column<string>(nullable: true),
                    SiteUrl = table.Column<string>(nullable: true),
                    TweeterUser = table.Column<string>(nullable: true),
                    YoutubeUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entities_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SecondaryPlans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: true),
                    AllowedUsers = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecondaryPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecondaryPlans_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Briefings",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AgeRange = table.Column<string>(nullable: true),
                    Analysis = table.Column<string>(maxLength: 2147483647, nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    ConnectedEntityId = table.Column<int>(nullable: true),
                    ConnectedEntityId1 = table.Column<long>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DocumentUrl = table.Column<string>(nullable: true),
                    Entity = table.Column<string>(nullable: true),
                    EntityId = table.Column<long>(nullable: false),
                    Factor = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Personality = table.Column<string>(nullable: true),
                    Report = table.Column<string>(nullable: true),
                    Report2 = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: true),
                    Strength = table.Column<decimal>(nullable: false),
                    Tone = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Briefings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Briefings_Entities_ConnectedEntityId1",
                        column: x => x.ConnectedEntityId1,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Briefings_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AdministratorId",
                table: "Accounts",
                column: "AdministratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_PlanId",
                table: "Accounts",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Argument_RelationId",
                table: "Argument",
                column: "RelationId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AccountId",
                table: "AspNetUsers",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PlanId",
                table: "AspNetUsers",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Author_MetadataId",
                table: "Author",
                column: "MetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_Briefings_ConnectedEntityId1",
                table: "Briefings",
                column: "ConnectedEntityId1");

            migrationBuilder.CreateIndex(
                name: "IX_Briefings_EntityId",
                table: "Briefings",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_NLUResultId",
                table: "Category",
                column: "NLUResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_StateId",
                table: "Cities",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Concept_NLUResultId",
                table: "Concept",
                column: "NLUResultId");

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
                name: "IX_DisambiguationSubtype_DisambiguationId",
                table: "DisambiguationSubtype",
                column: "DisambiguationId");

            migrationBuilder.CreateIndex(
                name: "IX_Emotion_EmotionDocumentId",
                table: "Emotion",
                column: "EmotionDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmotionDocument_EmotionDetailId",
                table: "EmotionDocument",
                column: "EmotionDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_EmotionTarget_EmotionDetailId",
                table: "EmotionTarget",
                column: "EmotionDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_EmotionTarget_EmotionId",
                table: "EmotionTarget",
                column: "EmotionId");

            migrationBuilder.CreateIndex(
                name: "IX_Entities_AccountId",
                table: "Entities",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_DisambiguationId",
                table: "Entity",
                column: "DisambiguationId");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_NLUResultId",
                table: "Entity",
                column: "NLUResultId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityRelation_ArgumentId",
                table: "EntityRelation",
                column: "ArgumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Features_PlanId",
                table: "Features",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Feed_MetadataId",
                table: "Feed",
                column: "MetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_Keywords_NLUResultId",
                table: "Keywords",
                column: "NLUResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Keywords_emotionsId",
                table: "Keywords",
                column: "emotionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Keywords_sentimentId",
                table: "Keywords",
                column: "sentimentId");

            migrationBuilder.CreateIndex(
                name: "IX_NluResults_EmotionId",
                table: "NluResults",
                column: "EmotionId");

            migrationBuilder.CreateIndex(
                name: "IX_NluResults_MetadataId",
                table: "NluResults",
                column: "MetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_NluResults_SentimentId",
                table: "NluResults",
                column: "SentimentId");

            migrationBuilder.CreateIndex(
                name: "IX_Personality_PersonalityResultId",
                table: "Personality",
                column: "PersonalityResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Relation_NLUResultId",
                table: "Relation",
                column: "NLUResultId");

            migrationBuilder.CreateIndex(
                name: "IX_ScrapedPages_ScrapSourceId",
                table: "ScrapedPages",
                column: "ScrapSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_SecondaryPlans_AccountId",
                table: "SecondaryPlans",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SemanticAction_VerbId",
                table: "SemanticAction",
                column: "VerbId");

            migrationBuilder.CreateIndex(
                name: "IX_SemanticRole_ActionId",
                table: "SemanticRole",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_SemanticRole_NLUResultId",
                table: "SemanticRole",
                column: "NLUResultId");

            migrationBuilder.CreateIndex(
                name: "IX_SemanticRole_ObjectId",
                table: "SemanticRole",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SemanticRole_SubjectId",
                table: "SemanticRole",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SentencesTone_ToneResultId",
                table: "SentencesTone",
                column: "ToneResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Sentiment_SentimentDocId",
                table: "Sentiment",
                column: "SentimentDocId");

            migrationBuilder.CreateIndex(
                name: "IX_SentimentTarget_SentimentId",
                table: "SentimentTarget",
                column: "SentimentId");

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryId",
                table: "States",
                column: "CountryId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Accounts_AccountId",
                table: "AspNetUsers",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AspNetUsers_AdministratorId",
                table: "Accounts");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Briefings");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Concept");

            migrationBuilder.DropTable(
                name: "ConsumptionDetail");

            migrationBuilder.DropTable(
                name: "Detail");

            migrationBuilder.DropTable(
                name: "DisambiguationSubtype");

            migrationBuilder.DropTable(
                name: "EmotionTarget");

            migrationBuilder.DropTable(
                name: "Entity");

            migrationBuilder.DropTable(
                name: "EntityMentions");

            migrationBuilder.DropTable(
                name: "EntityRelation");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Feed");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Industries");

            migrationBuilder.DropTable(
                name: "Keywords");

            migrationBuilder.DropTable(
                name: "Permitions");

            migrationBuilder.DropTable(
                name: "RelationTypes");

            migrationBuilder.DropTable(
                name: "ScrapedPages");

            migrationBuilder.DropTable(
                name: "ScrapQueue");

            migrationBuilder.DropTable(
                name: "SecondaryPlans");

            migrationBuilder.DropTable(
                name: "SemanticRole");

            migrationBuilder.DropTable(
                name: "SentimentTarget");

            migrationBuilder.DropTable(
                name: "Tone");

            migrationBuilder.DropTable(
                name: "Visitors");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Entities");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "ConsumptionPreferences");

            migrationBuilder.DropTable(
                name: "Personality");

            migrationBuilder.DropTable(
                name: "Disambiguation");

            migrationBuilder.DropTable(
                name: "Argument");

            migrationBuilder.DropTable(
                name: "EmotionsKeyword");

            migrationBuilder.DropTable(
                name: "SentimentKeyword");

            migrationBuilder.DropTable(
                name: "ScrapSources");

            migrationBuilder.DropTable(
                name: "SemanticAction");

            migrationBuilder.DropTable(
                name: "SemanticObject");

            migrationBuilder.DropTable(
                name: "SemanticSubject");

            migrationBuilder.DropTable(
                name: "ToneCategories");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Personalities");

            migrationBuilder.DropTable(
                name: "Relation");

            migrationBuilder.DropTable(
                name: "SemanticVerb");

            migrationBuilder.DropTable(
                name: "SentencesTone");

            migrationBuilder.DropTable(
                name: "NluResults");

            migrationBuilder.DropTable(
                name: "Tones");

            migrationBuilder.DropTable(
                name: "Emotion");

            migrationBuilder.DropTable(
                name: "Metadata");

            migrationBuilder.DropTable(
                name: "Sentiment");

            migrationBuilder.DropTable(
                name: "DocumentTone");

            migrationBuilder.DropTable(
                name: "EmotionDocument");

            migrationBuilder.DropTable(
                name: "SentimentDocument");

            migrationBuilder.DropTable(
                name: "EmotionDetail");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Plans");
        }
    }
}
