using System;
using System.Buffers;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Action.Models;
using Action.Models.Core;
//using Action.Services.AutoMapper;
using Action.Services.Scrap.Core;
using Action.Services.SMTP;
using Action.Services.TaskScheduler;
//using AutoMapper;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ScrapperV2 = Action.Services.Scrap.V2.Scrapper;

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
                    Path.Combine(Directory.GetCurrentDirectory(), "App_Data")));
            
            // add database context
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DataConnection"),
                    opt => opt.MigrationsAssembly("Action")));

            // Add framework services.
            services.AddMvc(options =>
            {
                options.OutputFormatters.Clear();
                options.OutputFormatters.Add(new JsonOutputFormatter(new JsonSerializerSettings(){
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                }, ArrayPool<char>.Shared));
            });
            services.AddCors(o => o.AddPolicy("Default", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader().Build();
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


           // services.AddAutoMapper(typeof(Startup));
           
            //Enables TaskManager
            services.AddHangfire(x => x.UseStorage(new MemoryStorage()));
        }

       

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseAuthentication();
            app.UseHangfireServer();
            app.UseCors("Default");
            app.UseMvc(routes => { });

            var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

            EnsureDatabaseCreated(dbContext);
            NotifyApplicationSupport();
        }

        private void NotifyApplicationSupport()
        {
            //SmtpService.SendMessage("luiz@nexo.ai", "[ACTION API INICIALIZATION]", "API INICIALIZADA");
        }

        private void EnsureDatabaseCreated(ApplicationDbContext dbContext)
        {
            
            
            
           //StartScraper(dbContext);
        }

        private void StartScraper(ApplicationDbContext dbContext)
        {
            var lMemoryStorage = new MemoryStorage();
            var lOptions = new BackgroundJobServerOptions();

            using (var server = new BackgroundJobServer(lOptions, lMemoryStorage))
            {
                /*JobStorage.Current = new MemoryStorage();
                
                RecurringJob.AddOrUpdate(
                    () => */
                //new ScrapService().StartScraperV2(dbContext);
                /*,
                    Cron.Daily());
                
                RecurringJob.AddOrUpdate(()=>*/
              //  Task.Run( ()=>  ApplicationTaskScheduler.ProccessDataExtraction(dbContext));/*, 
               //     Cron.Daily());*/
            }
        }
    }
}