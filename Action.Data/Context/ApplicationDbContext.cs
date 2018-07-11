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

namespace Action.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(this.optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
                builder.Entity<User>()
                .HasOne(x => x.Plan)
                .WithMany()
                .HasForeignKey(x => x.PlanId);

                builder.Entity<User>()
                .HasOne(x => x.Account)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.AccountId);

        }


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





    }
}
