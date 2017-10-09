using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Action.Models;

namespace Action.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.3");

            modelBuilder.Entity("Action.Models.Permition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActionName");

                    b.Property<int>("ActionPermition");

                    b.HasKey("Id");

                    b.ToTable("Permitions");
                });

            modelBuilder.Entity("Action.Models.Scrap.EntityMentions", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<long>("EntityId");

                    b.Property<int>("ScrapSourceId");

                    b.Property<Guid>("ScrapedPageId");

                    b.HasKey("Id");

                    b.ToTable("EntityMentions");
                });

            modelBuilder.Entity("Action.Models.Scrap.ScrapedPage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Hash");

                    b.Property<int?>("ScrapSourceId");

                    b.Property<int>("Status");

                    b.Property<string>("Text");

                    b.Property<string>("Translated");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("ScrapSourceId");

                    b.ToTable("ScrapedPages");
                });

            modelBuilder.Entity("Action.Models.Scrap.ScrapSource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alias");

                    b.Property<int>("Dept");

                    b.Property<string>("EndTag");

                    b.Property<int>("Limit");

                    b.Property<int>("PageStatus");

                    b.Property<string>("StarTag");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("ScrapSources");
                });

            modelBuilder.Entity("Action.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Action.Models.Visitor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Visitors");
                });

            modelBuilder.Entity("Action.Models.Watson.Entity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alias");

                    b.Property<int>("CategoryId");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Entities");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.Argument", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("RelationId");

                    b.Property<string>("text");

                    b.HasKey("Id");

                    b.HasIndex("RelationId");

                    b.ToTable("Argument");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("MetadataId");

                    b.Property<string>("name");

                    b.HasKey("Id");

                    b.HasIndex("MetadataId");

                    b.ToTable("Author");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Label");

                    b.Property<long?>("NLUResultId");

                    b.Property<float?>("Score");

                    b.HasKey("Id");

                    b.HasIndex("NLUResultId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.Concept", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Dbpedia_resource");

                    b.Property<long?>("NLUResultId");

                    b.Property<float?>("Relevance");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("NLUResultId");

                    b.ToTable("Concept");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.Disambiguation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Dbpedia_resource");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Disambiguation");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.DisambiguationSubtype", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("DisambiguationId");

                    b.Property<string>("SubType");

                    b.HasKey("Id");

                    b.HasIndex("DisambiguationId");

                    b.ToTable("DisambiguationSubtype");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.Emotion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("EmotionDocumentId");

                    b.HasKey("Id");

                    b.HasIndex("EmotionDocumentId");

                    b.ToTable("Emotion");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.EmotionDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float?>("Anger");

                    b.Property<float?>("Disgust");

                    b.Property<float?>("Fear");

                    b.Property<float?>("Joy");

                    b.Property<float?>("Sadness");

                    b.HasKey("Id");

                    b.ToTable("EmotionDetail");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.EmotionDocument", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("EmotionDetailId");

                    b.HasKey("Id");

                    b.HasIndex("EmotionDetailId");

                    b.ToTable("EmotionDocument");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.EmotionsKeyword", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float?>("anger");

                    b.Property<float?>("disgust");

                    b.Property<float?>("fear");

                    b.Property<float?>("joy");

                    b.Property<float?>("sadness");

                    b.HasKey("Id");

                    b.ToTable("EmotionsKeyword");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.EmotionTarget", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("EmotionDetailId");

                    b.Property<Guid?>("EmotionId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("EmotionDetailId");

                    b.HasIndex("EmotionId");

                    b.ToTable("EmotionTarget");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.Entity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("Count");

                    b.Property<Guid?>("DisambiguationId");

                    b.Property<long?>("NLUResultId");

                    b.Property<float?>("Relevance");

                    b.Property<string>("Text");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("DisambiguationId");

                    b.HasIndex("NLUResultId");

                    b.ToTable("Entity");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.EntityRelation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ArgumentId");

                    b.Property<string>("text");

                    b.Property<string>("type");

                    b.HasKey("Id");

                    b.HasIndex("ArgumentId");

                    b.ToTable("EntityRelation");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.Feed", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("MetadataId");

                    b.Property<string>("link");

                    b.HasKey("Id");

                    b.HasIndex("MetadataId");

                    b.ToTable("Feed");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.Keywords", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("NLUResultId");

                    b.Property<Guid?>("emotionsId");

                    b.Property<string>("language");

                    b.Property<float?>("relevance");

                    b.Property<string>("retrieved_url");

                    b.Property<Guid?>("sentimentId");

                    b.Property<string>("text");

                    b.HasKey("Id");

                    b.HasIndex("NLUResultId");

                    b.HasIndex("emotionsId");

                    b.HasIndex("sentimentId");

                    b.ToTable("Keywords");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.Metadata", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("image");

                    b.Property<string>("language");

                    b.Property<DateTime>("publication_date");

                    b.Property<string>("retrieved_url");

                    b.Property<string>("title");

                    b.HasKey("Id");

                    b.ToTable("Metadata");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.NLUResult", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("EmotionId");

                    b.Property<Guid?>("MetadataId");

                    b.Property<Guid>("ScrapedPageId");

                    b.Property<Guid?>("SentimentId");

                    b.HasKey("Id");

                    b.HasIndex("EmotionId");

                    b.HasIndex("MetadataId");

                    b.HasIndex("SentimentId");

                    b.ToTable("NluResults");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.Relation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("NLUResultId");

                    b.Property<string>("language");

                    b.Property<float?>("score");

                    b.Property<string>("sentence");

                    b.Property<string>("type");

                    b.HasKey("Id");

                    b.HasIndex("NLUResultId");

                    b.ToTable("Relation");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.SemanticAction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Normalized");

                    b.Property<string>("Text");

                    b.Property<Guid?>("VerbId");

                    b.HasKey("Id");

                    b.HasIndex("VerbId");

                    b.ToTable("SemanticAction");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.SemanticObject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("SemanticObject");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.SemanticRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ActionId");

                    b.Property<long?>("NLUResultId");

                    b.Property<Guid?>("ObjectId");

                    b.Property<string>("Sentence");

                    b.Property<Guid?>("SubjectId");

                    b.HasKey("Id");

                    b.HasIndex("ActionId");

                    b.HasIndex("NLUResultId");

                    b.HasIndex("ObjectId");

                    b.HasIndex("SubjectId");

                    b.ToTable("SemanticRole");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.SemanticSubject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("SemanticSubject");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.SemanticVerb", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Tense");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("SemanticVerb");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.Sentiment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("RetrievedUrl");

                    b.Property<Guid?>("SentimentDocId");

                    b.HasKey("Id");

                    b.HasIndex("SentimentDocId");

                    b.ToTable("Sentiment");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.SentimentDocument", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Label");

                    b.Property<float?>("Score");

                    b.HasKey("Id");

                    b.ToTable("SentimentDocument");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.SentimentKeyword", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float?>("score");

                    b.HasKey("Id");

                    b.ToTable("SentimentKeyword");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.SentimentTarget", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Label");

                    b.Property<float?>("Score");

                    b.Property<Guid?>("SentimentId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("SentimentId");

                    b.ToTable("SentimentTarget");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Action.Models.Scrap.ScrapedPage", b =>
                {
                    b.HasOne("Action.Models.Scrap.ScrapSource", "ScrapSource")
                        .WithMany()
                        .HasForeignKey("ScrapSourceId");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.Argument", b =>
                {
                    b.HasOne("Action.Services.Watson.NLU.Relation")
                        .WithMany("Arguments")
                        .HasForeignKey("RelationId");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.Author", b =>
                {
                    b.HasOne("Action.Services.Watson.NLU.Metadata")
                        .WithMany("Authors")
                        .HasForeignKey("MetadataId");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.Category", b =>
                {
                    b.HasOne("Action.Services.Watson.NLU.NLUResult")
                        .WithMany("Category")
                        .HasForeignKey("NLUResultId");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.Concept", b =>
                {
                    b.HasOne("Action.Services.Watson.NLU.NLUResult")
                        .WithMany("Concept")
                        .HasForeignKey("NLUResultId");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.DisambiguationSubtype", b =>
                {
                    b.HasOne("Action.Services.Watson.NLU.Disambiguation", "Disambiguation")
                        .WithMany("Subtype")
                        .HasForeignKey("DisambiguationId");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.Emotion", b =>
                {
                    b.HasOne("Action.Services.Watson.NLU.EmotionDocument", "EmotionDocument")
                        .WithMany()
                        .HasForeignKey("EmotionDocumentId");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.EmotionDocument", b =>
                {
                    b.HasOne("Action.Services.Watson.NLU.EmotionDetail", "EmotionDetail")
                        .WithMany()
                        .HasForeignKey("EmotionDetailId");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.EmotionTarget", b =>
                {
                    b.HasOne("Action.Services.Watson.NLU.EmotionDetail", "EmotionDetail")
                        .WithMany()
                        .HasForeignKey("EmotionDetailId");

                    b.HasOne("Action.Services.Watson.NLU.Emotion")
                        .WithMany("EmotionTarget")
                        .HasForeignKey("EmotionId");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.Entity", b =>
                {
                    b.HasOne("Action.Services.Watson.NLU.Disambiguation", "Disambiguation")
                        .WithMany()
                        .HasForeignKey("DisambiguationId");

                    b.HasOne("Action.Services.Watson.NLU.NLUResult")
                        .WithMany("Entity")
                        .HasForeignKey("NLUResultId");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.EntityRelation", b =>
                {
                    b.HasOne("Action.Services.Watson.NLU.Argument")
                        .WithMany("EntityRelations")
                        .HasForeignKey("ArgumentId");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.Feed", b =>
                {
                    b.HasOne("Action.Services.Watson.NLU.Metadata")
                        .WithMany("Feeds")
                        .HasForeignKey("MetadataId");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.Keywords", b =>
                {
                    b.HasOne("Action.Services.Watson.NLU.NLUResult")
                        .WithMany("Keywords")
                        .HasForeignKey("NLUResultId");

                    b.HasOne("Action.Services.Watson.NLU.EmotionsKeyword", "emotions")
                        .WithMany()
                        .HasForeignKey("emotionsId");

                    b.HasOne("Action.Services.Watson.NLU.SentimentKeyword", "sentiment")
                        .WithMany()
                        .HasForeignKey("sentimentId");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.NLUResult", b =>
                {
                    b.HasOne("Action.Services.Watson.NLU.Emotion", "Emotion")
                        .WithMany()
                        .HasForeignKey("EmotionId");

                    b.HasOne("Action.Services.Watson.NLU.Metadata", "Metadata")
                        .WithMany()
                        .HasForeignKey("MetadataId");

                    b.HasOne("Action.Services.Watson.NLU.Sentiment", "Sentiment")
                        .WithMany()
                        .HasForeignKey("SentimentId");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.Relation", b =>
                {
                    b.HasOne("Action.Services.Watson.NLU.NLUResult")
                        .WithMany("Relations")
                        .HasForeignKey("NLUResultId");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.SemanticAction", b =>
                {
                    b.HasOne("Action.Services.Watson.NLU.SemanticVerb", "Verb")
                        .WithMany()
                        .HasForeignKey("VerbId");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.SemanticRole", b =>
                {
                    b.HasOne("Action.Services.Watson.NLU.SemanticAction", "Action")
                        .WithMany()
                        .HasForeignKey("ActionId");

                    b.HasOne("Action.Services.Watson.NLU.NLUResult")
                        .WithMany("SemanticRoles")
                        .HasForeignKey("NLUResultId");

                    b.HasOne("Action.Services.Watson.NLU.SemanticObject", "Object")
                        .WithMany()
                        .HasForeignKey("ObjectId");

                    b.HasOne("Action.Services.Watson.NLU.SemanticSubject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.Sentiment", b =>
                {
                    b.HasOne("Action.Services.Watson.NLU.SentimentDocument", "SentimentDoc")
                        .WithMany()
                        .HasForeignKey("SentimentDocId");
                });

            modelBuilder.Entity("Action.Services.Watson.NLU.SentimentTarget", b =>
                {
                    b.HasOne("Action.Services.Watson.NLU.Sentiment")
                        .WithMany("SentimentTarget")
                        .HasForeignKey("SentimentId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Action.Models.User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Action.Models.User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Action.Models.User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
