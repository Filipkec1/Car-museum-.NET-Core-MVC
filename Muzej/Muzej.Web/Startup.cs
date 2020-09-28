using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Muzej.DAL;
using Muzej.Model;

namespace Muzej.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<MuzejManagerDbContext>(options =>
                options.UseSqlServer(
                Configuration.GetConnectionString("MuzejManagerDbContext")));

            services.AddIdentity<AppUser, IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<MuzejManagerDbContext>();

            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            var supportedCultures = new[]
{
             new CultureInfo("hr"), new CultureInfo("en-US")
             };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("hr"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default-car",
                    "Automobil/Index",
                    new { controller = "Car", action = "Index" }
                    );

                routes.MapRoute(
                    "default-create-empty",
                    "Automobil/Novi-Automobil",
                    new { controller = "Car", action = "Create" }
                    );

                routes.MapRoute(
                    "default-edit",
                    "Automobil/Uredi-Automobil/{id}",
                    new { controller = "Car", action = "Edit" , id = ""},
                    constraints: new
                    {
                        id = @"[0-9]*"
                    }
                    );


                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
