using System.Globalization;
using System.IO;
using BlingRus.Domain.Discounts;
using BlingRus.Domain.Ordering;
using BlingRus.Domain.Services;
using BlingRus.Domain.Shopping;
using BlingRus.Web.Models;
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
                .AddDataAnnotationsLocalization(opts => opts.DataAnnotationLocalizerProvider = 
                    (type, factory) => factory.Create(typeof(ModelResources)));
            services.AddDbContext<ShoppingContext>(options => options.UseSqlite("Filename=shopping.sqlite"));
            services.AddScoped<IShoppingContext, ShoppingContext>();
            services.AddScoped<CheckoutService, CheckoutService>();
            services.AddScoped<PricingModel, PricingModel>();
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
                DefaultRequestCulture = new RequestCulture("en"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Store}/{action=Index}/{id?}");
            });

            var inventoryJson = File.ReadAllText("Inventory.json");

            ShoppingInitializer.Initialize(shoppingContext, inventoryJson);
        }
    }
}
