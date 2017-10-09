using System.Net;
using System.Text;
using System.Threading.Tasks;
using Action.Filters;
using Action.Models;
using Action.Services.Scrap;
using Action.Services.Scrap.Repositories;
using Action.Services.Watson.NLU;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Action
{
    //[assembly: OwinStartup(typeof(Action.Startup))]
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            RootPath = env.ContentRootPath;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            Configuration = builder.Build();
        }

        public static string RootPath { get; set; }
        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkMySql();
            services.AddSingleton(provider => Configuration);

            // add database context
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DataConnection"),
                    opt => opt.MigrationsAssembly("Action")));

            ScrapperContext.ConnectionString = Configuration.GetConnectionString("DataConnection");
            NluService.Instance.AppContext = new ApplicationDbContext(
                new DbContextOptionsBuilder<ApplicationDbContext>().UseMySql(
                    Configuration.GetConnectionString("DataConnection"),
                    opt => opt.MigrationsAssembly("Action")).Options);

            // Add framework services.
            services.AddMvc();
            services.AddCors(o => o.AddPolicy("Default", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddIdentity<User, Role>(config => config.Cookies.ApplicationCookie.Events =
                    new CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = ctx =>
                        {
                            if (ctx.Request.Path.StartsWithSegments("/api"))
                                ctx.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                            return Task.FromResult(0);
                        }
                    }).AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            services.AddHangfire(x => x.UseStorage(new MemoryStorage()));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIdentity();
            app.UseHangfireDashboard("/tasks", new DashboardOptions
            {
                Authorization = new[] {new DashboardAuthorizeFilter()}
            });
            app.UseHangfireServer();
            app.UseCors("Default");
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration["JwtSecurityToken:Issuer"],
                    ValidAudience = Configuration["JwtSecurityToken:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecurityToken:Key"])),
                    ValidateLifetime = true
                }
            });
            app.UseMvc(routes => { });

            var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

            EnsureDatabaseCreated(dbContext);
            StartScraper();
        }

        private void EnsureDatabaseCreated(ApplicationDbContext dbContext)
        {
        }

        private void StartScraper()
        {
            var lMemoryStorage = new MemoryStorage();
            var lOptions = new BackgroundJobServerOptions();

            using (var server = new BackgroundJobServer(lOptions, lMemoryStorage))
            {
                JobStorage.Current = new MemoryStorage();
                RecurringJob.AddOrUpdate(
                    () => new ScrapService().StartScraper(),
                    Cron.MinuteInterval(60));
                
                RecurringJob.AddOrUpdate(
                    () => NluService.StartExtraction(),
                    Cron.MinuteInterval(120));
            }
        }
    }
}