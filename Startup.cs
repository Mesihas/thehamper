using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using HamperWeb.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HamperWeb.Models;
using Microsoft.Extensions.Configuration;

namespace HamperWeb
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            // add Identity services
            services.AddIdentity<IdentityUser, IdentityRole>(config =>
           {
               config.User.RequireUniqueEmail = false;
               config.Password.RequireDigit = true;
               config.Password.RequiredLength = 6;
               config.Password.RequireLowercase = false;
               config.Password.RequireUppercase = false;
               config.Password.RequireNonAlphanumeric = false;
           }).AddEntityFrameworkStores<MyDbContext>();

            services.AddDbContext<MyDbContext>();

            MongoDBContext.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
            MongoDBContext.DatabaseName = Configuration.GetSection("MongoConnection:DatabaseName").Value;
            MongoDBContext.IsSSL = Convert.ToBoolean(Configuration.GetSection("MongoConnection:IsSSL").Value);


            // add My services
            services.AddScoped<IDataService<Category>, DataService<Category>>();
            services.AddScoped<IDataService<Order>, DataService<Order>>();
            services.AddScoped<IDataService<Category>, DataService<Category>>();
            services.AddScoped<IDataService<Hamper>, DataService<Hamper>>();
            services.AddScoped<IDataService<Profile>, DataService<Profile>>();
            services.AddScoped<IDataService<Address>, DataService<Address>>();
            services.AddScoped<IDataService<OrderDetail>, DataService<OrderDetail>>();
            /////
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Configure session services
            // 1- add a default in-memory cache
            services.AddDistributedMemoryCache();
            // 2- add session with options
            services.AddSession();
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(10);
            //    options.CookieHttpOnly = true;
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseIdentity();
            app.UseMvcWithDefaultRoute();

            // check seed
            // SeedHelper.Seed(app.ApplicationServices )
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
