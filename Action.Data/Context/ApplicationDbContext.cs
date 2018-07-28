using Action.Data.Models.Core;
using Action.Data.Models.Core.Plans;
using Action.Data.Models.Core.Scrap;
using Action.Data.Models.Core.ServiceAccount;
using Action.Data.Models.Core.Watson;
using Action.Data.Models.Watson.NLU;
using Action.Data.Models.Core.Watson.PersonalityInsights;
using Action.Data.Models.Core.Watson.ToneAnalyze;
using Microsoft.EntityFrameworkCore;
using Entity = Action.Data.Models.Core.Watson.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Linq;

namespace Action.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {

        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Permition> Permitions { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<ScrapedPage> ScrapedPages { get; set; }
        public DbSet<ScrapSource> ScrapSources { get; set; }
        public DbSet<NLUResult> NluResults { get; set; }
        public DbSet<EntityMentions> EntityMentions { get; set; }
        public DbSet<PersonalityResult> Personalities { get; set; }
        public DbSet<ToneResult> Tones { get; set; }
        public DbSet<Briefing> Briefings { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<SecondaryPlan> SecondaryPlans { get; set; }
        public DbSet<ImageRepo> Images { get; set; }
        public DbSet<Relation> Relation { get; set; }
        public DbSet<RelationType> RelationTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ScrapQueue> ScrapQueue { get; set; }
        public DbSet<WatsonCredentials> WatsonCredentials { get; set; }
        public DbSet<BrickedSource> BrickedSources { get; set; }

        


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
         
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging( true);
            //base.OnConfiguring(this.optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {



            builder.Entity<EntityRole>()
                .HasKey(x => x.Id);
            builder.Entity<BriefingTag>()
                .HasKey(x => x.Id);
            builder.Entity<WatsonCredentials>()
                .HasKey(x => x.Service);


            builder.Entity<ImageRepo>()
                .Property(x => x.Base64Image)
                .HasMaxLength(Int32.MaxValue);

            builder.Entity<Account>()
                .HasMany(x => x.Users);


            builder.Entity<State>();

            builder.Entity<Account>()
                .HasOne(x => x.Plan)
                .WithMany()
                .HasForeignKey(x => x.PlanId);

            builder.Entity<Account>()
                .HasOne(x => x.Administrator)
                .WithMany()
                .HasForeignKey(x => x.AdministratorId);

            builder.Entity<Account>()
                .HasMany(x => x.SecondaryPlans);

            builder.Entity<Plan>()
                .HasMany(x => x.Features);

            builder.Entity<User>()
                .HasOne(x => x.Plan)
                .WithMany()
                .HasForeignKey(x => x.PlanId);

            builder.Entity<User>()
                .HasOne(x => x.Account)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.AccountId);

            builder.Entity<Briefing>()
                .Property(x => x.Analysis)
                .HasMaxLength(int.MaxValue);

            builder.Entity<Entity>()
                .HasMany(x => x.Briefings);

            builder.Entity<Entity>()
            .Property(x => x.RelatedRoles)
            .HasMaxLength(1000)
            .HasColumnType("varchar(1000)");

            builder.Entity<Entity>()
                   .HasMany(x => x.RelatedEntities)
                   .WithOne(x => x.CoreEntity)
                   .HasForeignKey(x => x.EntityId);

            builder.Entity<Entity>()
                   .HasOne(x => x.Industry)
                   .WithMany(i => i.Entities)
                   .HasForeignKey(x => x.IndustryId);


            builder.Entity<SecondaryPlan>()
                .HasOne(x => x.Account)
                .WithMany(x => x.SecondaryPlans)
                .HasForeignKey(x => x.AccountId);

            builder.Entity<City>()
                .HasOne(x => x.State)
                .WithMany()
                .HasForeignKey(x => x.StateId);


            builder.Entity<ScrapSourceIndustry>()
                  .HasKey(x => new { x.ScrapSourceId, x.IndustryId });

            builder.Entity<ScrapSourceIndustry>()
                .HasOne(x => x.ScrapSource)
                .WithMany(i => i.Industries)
                .HasForeignKey(x => x.ScrapSourceId);

            builder.Entity<ScrapSourceIndustry>()
            .HasOne(x => x.Industry)
            .WithMany(s => s.ScrapSources)
            .HasForeignKey(x => x.IndustryId);


            //entities x sources

            builder.Entity<ScrapSourceEntity>()
                .HasKey(x => new { x.ScrapSourceId, x.EntityId });

            builder.Entity<ScrapSourceEntity>()
                .HasOne(x => x.ScrapSource)
                .WithMany(i => i.Entities)
                .HasForeignKey(x => x.ScrapSourceId);

            builder.Entity<ScrapSourceEntity>()
            .HasOne(x => x.Entity)
            .WithMany(s => s.ScrapSources)
            .HasForeignKey(x => x.EntityId);






            base.OnModelCreating(builder);
        }






    }
}
