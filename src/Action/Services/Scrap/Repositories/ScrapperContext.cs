using Action.Models.Scrap;
using Microsoft.EntityFrameworkCore;

namespace Action.Services.Scrap.Repositories
{
    public class ScrapperContext : DbContext
    {
        private static readonly DbContextOptions options =
            new DbContextOptionsBuilder<ScrapperContext>().UseMySql(ConnectionString).Options;

        public ScrapperContext() : base(options)
        {
        }


        public static string ConnectionString { get; set; } =
            "Server=sl-us-south-1-portal.2.dblayer.com;port=23818;Database=compose;Uid=admin;Pwd=KQCVOHDRPOEPUSXN;";

        public DbSet<ScrapedPage> ScrapedPages { get; set; }
        public DbSet<ScrapSource> ScrapSources { get; set; }

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