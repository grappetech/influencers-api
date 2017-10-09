using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Action.Migrations
{
    public partial class NluExtract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntityMentions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
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
                name: "Disambiguation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Dbpedia_resource = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disambiguation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmotionDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
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
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
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
                name: "Metadata",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
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
                name: "SemanticObject",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
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
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
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
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
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
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
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
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    score = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentimentKeyword", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DisambiguationSubtype",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
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
                name: "EmotionDocument",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
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
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
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
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
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
                name: "SemanticAction",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
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
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
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
                name: "Emotion",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
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
                name: "SentimentTarget",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
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
                name: "EmotionTarget",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
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
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
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
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Label = table.Column<string>(nullable: true),
                    NLUResultId = table.Column<long>(nullable: true),
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
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Dbpedia_resource = table.Column<string>(nullable: true),
                    NLUResultId = table.Column<long>(nullable: true),
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
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Count = table.Column<long>(nullable: true),
                    DisambiguationId = table.Column<Guid>(nullable: true),
                    NLUResultId = table.Column<long>(nullable: true),
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
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    NLUResultId = table.Column<long>(nullable: true),
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
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    NLUResultId = table.Column<long>(nullable: true),
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
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    ActionId = table.Column<Guid>(nullable: true),
                    NLUResultId = table.Column<long>(nullable: true),
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
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
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
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
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

            migrationBuilder.AddColumn<string>(
                name: "Hash",
                table: "ScrapedPages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Argument_RelationId",
                table: "Argument",
                column: "RelationId");

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
                name: "IX_Relation_NLUResultId",
                table: "Relation",
                column: "NLUResultId");

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
                name: "IX_Sentiment_SentimentDocId",
                table: "Sentiment",
                column: "SentimentDocId");

            migrationBuilder.CreateIndex(
                name: "IX_SentimentTarget_SentimentId",
                table: "SentimentTarget",
                column: "SentimentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hash",
                table: "ScrapedPages");

            migrationBuilder.DropTable(
                name: "EntityMentions");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Concept");

            migrationBuilder.DropTable(
                name: "DisambiguationSubtype");

            migrationBuilder.DropTable(
                name: "EmotionTarget");

            migrationBuilder.DropTable(
                name: "Entity");

            migrationBuilder.DropTable(
                name: "EntityRelation");

            migrationBuilder.DropTable(
                name: "Feed");

            migrationBuilder.DropTable(
                name: "Keywords");

            migrationBuilder.DropTable(
                name: "SemanticRole");

            migrationBuilder.DropTable(
                name: "SentimentTarget");

            migrationBuilder.DropTable(
                name: "Disambiguation");

            migrationBuilder.DropTable(
                name: "Argument");

            migrationBuilder.DropTable(
                name: "EmotionsKeyword");

            migrationBuilder.DropTable(
                name: "SentimentKeyword");

            migrationBuilder.DropTable(
                name: "SemanticAction");

            migrationBuilder.DropTable(
                name: "SemanticObject");

            migrationBuilder.DropTable(
                name: "SemanticSubject");

            migrationBuilder.DropTable(
                name: "Relation");

            migrationBuilder.DropTable(
                name: "SemanticVerb");

            migrationBuilder.DropTable(
                name: "NluResults");

            migrationBuilder.DropTable(
                name: "Emotion");

            migrationBuilder.DropTable(
                name: "Metadata");

            migrationBuilder.DropTable(
                name: "Sentiment");

            migrationBuilder.DropTable(
                name: "EmotionDocument");

            migrationBuilder.DropTable(
                name: "SentimentDocument");

            migrationBuilder.DropTable(
                name: "EmotionDetail");
        }
    }
}
