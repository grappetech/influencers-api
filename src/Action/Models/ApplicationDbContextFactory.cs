using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Action.Models
{
    public class ApplicationDbContextFactory: IDesignTimeDbContextFactory<ApplicationDbContext>
    {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySql("Server=sl-us-south-1-portal.0.dblayer.com;port=24035;Database=connection;Uid=admin;Pwd=RXIVJFYGQLTNOJJA;");
            //optionsBuilder.UseMySql("Server=localhost;port=3306;Database=connection;Uid=root;Pwd=Projetos18;");
                return new ApplicationDbContext(optionsBuilder.Options);
            }
    }
}