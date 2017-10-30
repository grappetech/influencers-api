using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Action.Migrations
{
    public partial class UserPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(127)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Briefings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Analysis = table.Column<string>(type: "longtext", nullable: true),
                    Brand = table.Column<string>(type: "longtext", nullable: true),
                    Description = table.Column<string>(type: "longtext", nullable: true),
                    Factor = table.Column<string>(type: "longtext", nullable: true),
                    File = table.Column<byte[]>(type: "longblob", nullable: true),
                    Product = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Briefings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Disambiguation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Dbpedia_resource = table.Column<string>(type: "longtext", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disambiguation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTone",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTone", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmotionDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Anger = table.Column<float>(type: "float", nullable: true),
                    Disgust = table.Column<float>(type: "float", nullable: true),
                    Fear = table.Column<float>(type: "float", nullable: true),
                    Joy = table.Column<float>(type: "float", nullable: true),
                    Sadness = table.Column<float>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmotionDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmotionsKeyword",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    anger = table.Column<float>(type: "float", nullable: true),
                    disgust = table.Column<float>(type: "float", nullable: true),
                    fear = table.Column<float>(type: "float", nullable: true),
                    joy = table.Column<float>(type: "float", nullable: true),
                    sadness = table.Column<float>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmotionsKeyword", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Alias = table.Column<string>(type: "longtext", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FacebookUser = table.Column<string>(type: "longtext", nullable: true),
                    InstagranUser = table.Column<string>(type: "longtext", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    PictureUrl = table.Column<string>(type: "longtext", nullable: true),
                    SiteUrl = table.Column<string>(type: "longtext", nullable: true),
                    TweeterUser = table.Column<string>(type: "longtext", nullable: true),
                    YoutubeUser = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityMentions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EntityId = table.Column<long>(type: "bigint", nullable: false),
                    ScrapSourceId = table.Column<int>(type: "int", nullable: false),
                    ScrapedPageId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityMentions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Metadata",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    image = table.Column<string>(type: "longtext", nullable: true),
                    language = table.Column<string>(type: "longtext", nullable: true),
                    publication_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    retrieved_url = table.Column<string>(type: "longtext", nullable: true),
                    title = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metadata", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ActionName = table.Column<string>(type: "longtext", nullable: true),
                    ActionPermition = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personalities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    EntityId = table.Column<long>(type: "bigint", nullable: false),
                    ScrapSourceId = table.Column<int>(type: "int", nullable: false),
                    ScrapedPageId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(65, 30)", nullable: false),
                    Slug = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScrapSources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Alias = table.Column<string>(type: "longtext", nullable: true),
                    Dept = table.Column<int>(type: "int", nullable: false),
                    EndTag = table.Column<string>(type: "longtext", nullable: true),
                    Limit = table.Column<int>(type: "int", nullable: false),
                    PageStatus = table.Column<int>(type: "int", nullable: false),
                    StarTag = table.Column<string>(type: "longtext", nullable: true),
                    Url = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScrapSources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SemanticObject",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Text = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemanticObject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SemanticSubject",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Text = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemanticSubject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SemanticVerb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Tense = table.Column<string>(type: "longtext", nullable: true),
                    Text = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemanticVerb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SentimentDocument",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Label = table.Column<string>(type: "longtext", nullable: true),
                    Score = table.Column<float>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentimentDocument", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SentimentKeyword",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    score = table.Column<float>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentimentKeyword", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Visitors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true),
                    RoleId = table.Column<string>(type: "varchar(127)", nullable: false)
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
                name: "DisambiguationSubtype",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    DisambiguationId = table.Column<Guid>(type: "char(36)", nullable: true),
                    SubType = table.Column<string>(type: "longtext", nullable: true)
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
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    DocumentToneId = table.Column<Guid>(type: "char(36)", nullable: true),
                    EntityId = table.Column<long>(type: "bigint", nullable: false),
                    ScrapSourceId = table.Column<int>(type: "int", nullable: false),
                    ScrapedPageId = table.Column<Guid>(type: "char(36)", nullable: false)
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
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    EmotionDetailId = table.Column<Guid>(type: "char(36)", nullable: true)
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
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    MetadataId = table.Column<Guid>(type: "char(36)", nullable: true),
                    name = table.Column<string>(type: "longtext", nullable: true)
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
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    MetadataId = table.Column<Guid>(type: "char(36)", nullable: true),
                    link = table.Column<string>(type: "longtext", nullable: true)
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
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    ConsumptionPreferenceId = table.Column<string>(type: "longtext", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    PersonalityResultId = table.Column<Guid>(type: "char(36)", nullable: true)
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
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    Percentile = table.Column<double>(type: "double", nullable: true),
                    PersonalityResultId = table.Column<Guid>(type: "char(36)", nullable: true)
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
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(127)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: true),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true),
                    Surname = table.Column<string>(type: "longtext", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
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
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "longtext", nullable: true),
                    PlanId = table.Column<int>(type: "int", nullable: true)
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
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Hash = table.Column<string>(type: "longtext", nullable: true),
                    ScrapSourceId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "longtext", nullable: true),
                    Translated = table.Column<string>(type: "longtext", nullable: true),
                    Url = table.Column<string>(type: "longtext", nullable: true)
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
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Normalized = table.Column<string>(type: "longtext", nullable: true),
                    Text = table.Column<string>(type: "longtext", nullable: true),
                    VerbId = table.Column<Guid>(type: "char(36)", nullable: true)
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
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    RetrievedUrl = table.Column<string>(type: "longtext", nullable: true),
                    SentimentDocId = table.Column<Guid>(type: "char(36)", nullable: true)
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
                name: "SentencesTone",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    InputFrom = table.Column<long>(type: "bigint", nullable: true),
                    InputTo = table.Column<long>(type: "bigint", nullable: true),
                    SentenceId = table.Column<long>(type: "bigint", nullable: true),
                    Text = table.Column<string>(type: "longtext", nullable: true),
                    ToneResultId = table.Column<Guid>(type: "char(36)", nullable: true)
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
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    EmotionDocumentId = table.Column<Guid>(type: "char(36)", nullable: true)
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
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    ConsumptionPreferencesId = table.Column<Guid>(type: "char(36)", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false)
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
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    Percentile = table.Column<double>(type: "double", nullable: true),
                    PersonalityId = table.Column<Guid>(type: "char(36)", nullable: true),
                    PersonalityResultId = table.Column<Guid>(type: "char(36)", nullable: true),
                    PersonalityResultId1 = table.Column<Guid>(type: "char(36)", nullable: true)
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
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<string>(type: "varchar(127)", nullable: false)
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
                    LoginProvider = table.Column<string>(type: "varchar(127)", nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(127)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<string>(type: "varchar(127)", nullable: false)
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
                    UserId = table.Column<string>(type: "varchar(127)", nullable: false),
                    RoleId = table.Column<string>(type: "varchar(127)", nullable: false)
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
                    UserId = table.Column<string>(type: "varchar(127)", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(127)", nullable: false),
                    Name = table.Column<string>(type: "varchar(127)", nullable: false),
                    Value = table.Column<string>(type: "longtext", nullable: true)
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
                name: "SentimentTarget",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Label = table.Column<string>(type: "longtext", nullable: true),
                    Score = table.Column<float>(type: "float", nullable: true),
                    SentimentId = table.Column<Guid>(type: "char(36)", nullable: true),
                    Text = table.Column<string>(type: "longtext", nullable: true)
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
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    CategoryId = table.Column<string>(type: "longtext", nullable: true),
                    CategoryName = table.Column<string>(type: "longtext", nullable: true),
                    DocumentToneId = table.Column<Guid>(type: "char(36)", nullable: true),
                    SentencesToneId = table.Column<Guid>(type: "char(36)", nullable: true)
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
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    EmotionDetailId = table.Column<Guid>(type: "char(36)", nullable: true),
                    EmotionId = table.Column<Guid>(type: "char(36)", nullable: true),
                    Text = table.Column<string>(type: "longtext", nullable: true)
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
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmotionId = table.Column<Guid>(type: "char(36)", nullable: true),
                    MetadataId = table.Column<Guid>(type: "char(36)", nullable: true),
                    ScrapedPageId = table.Column<Guid>(type: "char(36)", nullable: false),
                    SentimentId = table.Column<Guid>(type: "char(36)", nullable: true)
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
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Score = table.Column<double>(type: "double", nullable: true),
                    ToneCategoriesId = table.Column<Guid>(type: "char(36)", nullable: true),
                    ToneId = table.Column<string>(type: "longtext", nullable: true),
                    ToneName = table.Column<string>(type: "longtext", nullable: true)
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
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Label = table.Column<string>(type: "longtext", nullable: true),
                    NLUResultId = table.Column<long>(type: "bigint", nullable: true),
                    Score = table.Column<float>(type: "float", nullable: true)
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
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Dbpedia_resource = table.Column<string>(type: "longtext", nullable: true),
                    NLUResultId = table.Column<long>(type: "bigint", nullable: true),
                    Relevance = table.Column<float>(type: "float", nullable: true),
                    Text = table.Column<string>(type: "longtext", nullable: true)
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
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Count = table.Column<long>(type: "bigint", nullable: true),
                    DisambiguationId = table.Column<Guid>(type: "char(36)", nullable: true),
                    NLUResultId = table.Column<long>(type: "bigint", nullable: true),
                    Relevance = table.Column<float>(type: "float", nullable: true),
                    Text = table.Column<string>(type: "longtext", nullable: true),
                    Type = table.Column<string>(type: "longtext", nullable: true)
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
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    NLUResultId = table.Column<long>(type: "bigint", nullable: true),
                    emotionsId = table.Column<Guid>(type: "char(36)", nullable: true),
                    language = table.Column<string>(type: "longtext", nullable: true),
                    relevance = table.Column<float>(type: "float", nullable: true),
                    retrieved_url = table.Column<string>(type: "longtext", nullable: true),
                    sentimentId = table.Column<Guid>(type: "char(36)", nullable: true),
                    text = table.Column<string>(type: "longtext", nullable: true)
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
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    NLUResultId = table.Column<long>(type: "bigint", nullable: true),
                    language = table.Column<string>(type: "longtext", nullable: true),
                    score = table.Column<float>(type: "float", nullable: true),
                    sentence = table.Column<string>(type: "longtext", nullable: true),
                    type = table.Column<string>(type: "longtext", nullable: true)
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
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    ActionId = table.Column<Guid>(type: "char(36)", nullable: true),
                    NLUResultId = table.Column<long>(type: "bigint", nullable: true),
                    ObjectId = table.Column<Guid>(type: "char(36)", nullable: true),
                    Sentence = table.Column<string>(type: "longtext", nullable: true),
                    SubjectId = table.Column<Guid>(type: "char(36)", nullable: true)
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
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    RelationId = table.Column<Guid>(type: "char(36)", nullable: true),
                    text = table.Column<string>(type: "longtext", nullable: true)
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
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    ArgumentId = table.Column<Guid>(type: "char(36)", nullable: true),
                    text = table.Column<string>(type: "longtext", nullable: true),
                    type = table.Column<string>(type: "longtext", nullable: true)
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
                name: "IX_Category_NLUResultId",
                table: "Category",
                column: "NLUResultId");

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
                name: "Entities");

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
                name: "Keywords");

            migrationBuilder.DropTable(
                name: "Permitions");

            migrationBuilder.DropTable(
                name: "ScrapedPages");

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
                name: "AspNetUsers");

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
                name: "Plans");

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
        }
    }
}
