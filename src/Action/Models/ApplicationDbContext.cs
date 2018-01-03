using System.Collections.Immutable;
using Action.Models.Plans;
using Action.Models.Scrap;
using Action.Models.ServiceAccount;
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
		public DbSet<Plan> Plans { get; set; }
		public DbSet<Account> Accounts { get; set; }
		public DbSet<Industry> Industries { get; set; }
		public DbSet<SecondaryPlan> SecondaryPlans { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{

			builder.Entity<Account>()
				.HasMany(x => x.Users);

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

			builder.Entity<ScrapedPage>()
				.HasOne(x => x.ScrapSource)
				.WithMany()
				.HasForeignKey(x => x.ScrapSourceId);

			builder.Entity<User>()
				.HasOne(x => x.Plan)
				.WithMany()
				.HasForeignKey(x => x.PlanId);

			builder.Entity<User>()
				.HasOne(x => x.Account)
				.WithMany(x => x.Users)
				.HasForeignKey(x => x.AccountId);

			builder.Entity<SecondaryPlan>()
				.HasOne(x => x.Account)
				.WithMany(x => x.SecondaryPlans)
				.HasForeignKey(x => x.AccountId);

			base.OnModelCreating(builder);
		}
	}
}