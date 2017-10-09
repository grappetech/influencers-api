using Action.Models.Scrap;
using Action.Services.Watson.NLU;
using Action.Services.Watson.PersonalityInsights;
using Action.Services.Watson.ToneAnalyze;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Entity = Action.Models.Watson.Entity;

namespace Action.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ScrapedPage>()
                .HasOne(x => x.ScrapSource)
                .WithMany()
                .HasForeignKey(x => x.ScrapSourceId);
            base.OnModelCreating(builder);
        }
    }
}