using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Action.Filters;
using Action.Models;
using Action.Services.Scrap;
using Action.Services.Scrap.Repositories;
using Action.Services.SMTP;
using Action.Services.Watson.NLU;
using Action.Services.Watson.PersonalityInsights;
using Action.Services.Watson.ToneAnalyze;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
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

            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
            
            // add database context
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DataConnection"),
                    opt => opt.MigrationsAssembly("Action")));

            //ScrapperContext.ConnectionString = Configuration.GetConnectionString("DataConnection");
            NluService.Instance.AppContext = new ApplicationDbContext(
                new DbContextOptionsBuilder<ApplicationDbContext>().UseMySql(
                    Configuration.GetConnectionString("DataConnection"),
                    opt => opt.MigrationsAssembly("Action")).Options);
            
            //ScrapperContext.ConnectionString = Configuration.GetConnectionString("DataConnection");
            PersonalityService.Instance.AppContext = new ApplicationDbContext(
                new DbContextOptionsBuilder<ApplicationDbContext>().UseMySql(
                    Configuration.GetConnectionString("DataConnection"),
                    opt => opt.MigrationsAssembly("Action")).Options);
            
            //ScrapperContext.ConnectionString = Configuration.GetConnectionString("DataConnection");
            ToneService.Instance.AppContext = new ApplicationDbContext(
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

            //Enables Authentication
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication().AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration["JwtSecurityToken:Issuer"],
                    ValidAudience = Configuration["JwtSecurityToken:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecurityToken:Key"])),
                    ValidateLifetime = true
                };
            });


            //ConfigureSmtp
            services.Configure<SmtpConfiguration>(Configuration.GetSection("SmtpConfiguration"));
            SmtpConfiguration.Configure(Configuration["SmtpConfiguration:Host"],
                Convert.ToInt32(Configuration["SmtpConfiguration:Port"]),
                Configuration["SmtpConfiguration:UserName"],
                Configuration["SmtpConfiguration:Password"],
                Configuration["SmtpConfiguration:Sender"],
                Convert.ToBoolean(Configuration["SmtpConfiguration:IsSSL"]));
            
            
            //Enables TaskManager
            services.AddHangfire(x => x.UseStorage(new MemoryStorage()));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseAuthentication();
            app.UseHangfireDashboard("/tasks", new DashboardOptions
            {
                Authorization = new[] {new DashboardAuthorizeFilter()}
            });
            app.UseHangfireServer();
            app.UseCors("Default");
            app.UseMvc(routes => { });

            var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

            EnsureDatabaseCreated(dbContext);
            NotifyApplicationSupport();
            StartScraper();
        }

        private void NotifyApplicationSupport()
        {
            //SmtpService.SendMessage("luiz@nexo.ai", "[ACTION API INICIALIZATION]", "API INICIALIZADA");
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