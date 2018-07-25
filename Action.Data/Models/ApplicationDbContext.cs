//using Action.Data.Models.Core.Plans;
//using Action.Data.Models.Core.Scrap;
//using Action.Data.Models.Core.ServiceAccount;
//using Action.Data.Models.Core.Watson;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
//using System;
//using Entity = Action.Data.Models.Core.Watson.Entity;

//namespace Action.Data.Models.Core
//{
//    public partial class ApplicationDbContext : IdentityDbContext<User>
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
//        {
//        }

//        protected override void OnModelCreating(ModelBuilder builder)
//        {
//            builder.Entity<EntityRole>()
//                .HasKey(x => x.Id);
//            builder.Entity<BriefingTag>()
//                .HasKey(x => x.Id);
//            builder.Entity<WatsonCredentials>()
//                .HasKey(x => x.Service);


//            builder.Entity<ImageRepo>()
//                .Property(x => x.Base64Image)
//                .HasMaxLength(Int32.MaxValue);

//            builder.Entity<Account>()
//                .HasMany(x => x.Users);


//            builder.Entity<State>();

//            builder.Entity<Account>()
//                .HasOne(x => x.Plan)
//                .WithMany()
//                .HasForeignKey(x => x.PlanId);

//            builder.Entity<Account>()
//                .HasOne(x => x.Administrator)
//                .WithMany()
//                .HasForeignKey(x => x.AdministratorId);

//            builder.Entity<Account>()
//                .HasMany(x => x.SecondaryPlans);

//            builder.Entity<Plan>()
//                .HasMany(x => x.Features);

//            builder.Entity<User>()
//                .HasOne(x => x.Plan)
//                .WithMany()
//                .HasForeignKey(x => x.PlanId);

//            builder.Entity<User>()
//                .HasOne(x => x.Account)
//                .WithMany(x => x.Users)
//                .HasForeignKey(x => x.AccountId);

//            builder.Entity<Briefing>()
//                .Property(x => x.Analysis)
//                .HasMaxLength(int.MaxValue);

//            builder.Entity<Entity>()
//                .HasMany(x => x.Briefings);

//            builder.Entity<Entity>()
//                   .HasMany(x => x.RelatedEntities)
//                   .WithOne(x => x.CoreEntity)
//                   .HasForeignKey(x => x.EntityId);

//            builder.Entity<SecondaryPlan>()
//                .HasOne(x => x.Account)
//                .WithMany(x => x.SecondaryPlans)
//                .HasForeignKey(x => x.AccountId);

//            builder.Entity<City>()
//                .HasOne(x => x.State)
//                .WithMany()
//                .HasForeignKey(x => x.StateId);



//            builder.Entity<ScrapSourceIndustry>()
//                .HasKey(x => new { x.ScrapSourceId, x.IndustryId });

//            builder.Entity<ScrapSourceIndustry>()
//                .HasOne(x => x.Industry)
//                .WithMany(i => i.ScrapSources)
//                .HasForeignKey(x => x.ScrapSourceId);

//            builder.Entity<ScrapSourceIndustry>()
//            .HasOne(x => x.ScrapSource)
//            .WithMany(s => s.Industries)
//            .HasForeignKey(x => x.IndustryId);



//            base.OnModelCreating(builder);
//        }
//    }
//}