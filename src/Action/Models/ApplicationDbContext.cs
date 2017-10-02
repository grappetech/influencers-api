using Action.Models.Scrap;
using Action.Models.Watson;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Action.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Permition> Permitions { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<ScrapedPage> ScrapedPages { get; set; }
        public DbSet<ScrapSource> ScrapSources { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

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
