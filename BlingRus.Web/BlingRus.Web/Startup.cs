﻿using System.Globalization;
using System.IO;
using BlingRus.Domain;
using BlingRus.Domain.Discounts;
using BlingRus.Domain.Services;
using BlingRus.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BlingRus.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddLocalization();
            services.AddMvc()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();
            services.AddDbContext<ShoppingContext>(options => options.UseSqlite("Filename=shopping.sqlite"));
            services.AddScoped<IShoppingContext, ShoppingContext>();
            services.AddScoped<CheckoutService, CheckoutService>();
            services.AddScoped<DiscountModel, DiscountModel>();
            services.AddScoped<IViewRenderService, ViewRenderService>();
            services.AddTransient<IMailService>(service => new SendGridMailService(Configuration.GetValue<string>("SendGrid:ApiKey"), service.GetRequiredService<IViewRenderService>()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ShoppingContext shoppingContext)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var supportedCultures = new[]
            {
                new CultureInfo("sv"),
                new CultureInfo("en")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("sv"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Landing}/{action=Index}/{id?}");
            });

            var inventoryJson = File.ReadAllText("Inventory.json");

            ShoppingInitializer.Initialize(shoppingContext, inventoryJson);
        }
    }
}
