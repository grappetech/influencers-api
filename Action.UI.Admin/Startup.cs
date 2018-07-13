using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Action.Data.Context;
using Action.Data.Models.Core.Watson;
using Action.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ActionUI.Admin.ExtensionsMethods;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Action.Data.Models.Core;
using Action.Services.SMTP;
using System.Globalization;

namespace ActionUI.Admin
{
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

            ConfigureAutoMapper();
        }

        private void ConfigureAutoMapper()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {

                cfg.CreateMap<ViewModel.Entity, Entity>().IgnoreAllUnmapped();
            });
        }

        public static string RootPath { get; set; }
        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkMySql();
            services.AddSingleton(provider => Configuration);

            services.AddDbContext<ApplicationDbContext>(options =>
              options.UseMySql(Configuration.GetConnectionString("DataConnection")));

            //Enables Authentication
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddScoped(typeof(Action.Repository.Base.IRepository<>), typeof(Action.Repository.Base.Repository<>));

            //services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });


            //custom services
            ConfigureServiceDI(services);


            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("pt-BR");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("pt-BR"),new CultureInfo("en-US") };
            });




            //ConfigureSmtp
            services.Configure<SmtpConfiguration>(Configuration.GetSection("SmtpConfiguration"));
            SmtpConfiguration.Configure(Configuration["SmtpConfiguration:Host"],
                Convert.ToInt32(Configuration["SmtpConfiguration:Port"]),
                Configuration["SmtpConfiguration:UserName"],
                Configuration["SmtpConfiguration:Password"],
                Configuration["SmtpConfiguration:Sender"],
                Convert.ToBoolean(Configuration["SmtpConfiguration:IsSSL"]));


            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AddPageRoute("/Login", "");

                    options.Conventions.AddFolderApplicationModelConvention("/Logged",
                        model => model.Filters.Add(new CustomFilter.UserLoggedFilter()));
                });
        }

        private void ConfigureServiceDI(IServiceCollection services)
        {
            services.AddTransient<EntityService>();
            services.AddTransient<WatsonEntityService>();
            services.AddTransient<IndustryService>();
            services.AddTransient<SourceService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseSession();


            app.UseMvc();
        }
    }
}
