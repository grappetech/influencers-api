using System;
using Action.Models.Core;
using Action.Models.Plans;
using Action.Models.Scrap;
using Action.Models.ServiceAccount;
using Action.Models.Watson;
using Action.Models.Watson.NLU;
using Action.Models.Watson.PersonalityInsights;
using Action.Models.Watson.ToneAnalyze;
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
		public DbSet<ImageRepo> Images { get; set; }
		public DbSet<Relation> Relation { get; set; }
		public DbSet<RelationType> RelationTypes { get; set; }
		public DbSet<Country> Countries { get; set; }
		public DbSet<State> States { get; set; }
		public DbSet<City> Cities { get; set; }
		public DbSet<ScrapQueue> ScrapQueue { get; set; }
		public DbSet<WatsonCredentials> WatsonCredentials { get; set; }
		
		protected override void OnModelCreating(ModelBuilder builder)
		{
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

			builder.Entity<SecondaryPlan>()
				.HasOne(x => x.Account)
				.WithMany(x => x.SecondaryPlans)
				.HasForeignKey(x => x.AccountId);

			builder.Entity<City>()
				.HasOne(x => x.State)
				.WithMany()
				.HasForeignKey(x => x.StateId);
			

			base.OnModelCreating(builder);
		}
	}
}