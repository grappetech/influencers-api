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
	public partial class ApplicationDbContext
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
		
	}
}