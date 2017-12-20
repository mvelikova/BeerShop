using System;
using System.Threading.Tasks;
using AutoMapper;
using Beershop.Data.Models;
using Beershop.Services;
using BeerShop.Data;
using BeerShop.Services.Contracts;
using BeerShop.Services.Html;
using BeerShop.Services.Html.Implementations;
using BeerShop.Services.Implementations;
using BeerShop.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BeerShop.Web
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
            services.AddDbContext<BeerShopDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    // Password settings
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 3;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequiredUniqueChars = 2;
                  
                    //user settings
                    options.User.RequireUniqueEmail = true;
                   


                })
                .AddEntityFrameworkStores<BeerShopDbContext>()
                .AddDefaultTokenProviders();
            //            services.AddIdentity<ApplicationUser, IdentityRole>()
            //                .AddEntityFrameworkStores<BeerShopDbContext>()
            //                .AddDefaultTokenProviders();
//            services.AddIdentity<ApplicationUser, IdentityRole>()
//                .AddEntityFrameworkStores<BeerShopDbContext>()
//                .AddDefaultTokenProviders();

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {

                facebookOptions.AppId = this.Configuration.GetSection("AppKeys")["FacebookAppId"];
                facebookOptions.AppSecret = this.Configuration.GetSection("AppKeys")["FacebookAppSecret"];
                facebookOptions.Scope.Add("public_profile");
                facebookOptions.Fields.Add("name");

                
            });
            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IBeerService, BeerService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IHtmlService, HtmlService>();
            services.AddScoped<IBeerCommentService, BeerCommentService>();
            services.AddScoped<IEventCommentService, EventCommentService>();


            services.AddAutoMapper();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
      
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "areas",
                    "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            using (var serviceScope =
                app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<BeerShopDbContext>();
                context.Database.Migrate();

                CreateRoles(serviceProvider).Wait();

            }
        }
        public async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles 
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                    await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            //Here you could create a super user who will maintain the web app
            
            var email = this.Configuration.GetSection("UserSettings")["AdminEmail"];

            var superUser = new ApplicationUser
            {
                
                Email = email,
                UserName="admin"
            };

            //Ensure you have these values in your appsettings.json or secrets.json file
            var userPwd = this.Configuration.GetSection("UserSettings")["AdminPassword"];
            var user = await userManager.FindByEmailAsync(
                this.Configuration.GetSection("UserSettings")["AdminEmail"]);

            if (user == null)
            {
                var createSuperUser = await userManager.CreateAsync(superUser, userPwd);
                if (createSuperUser.Succeeded)
                    await userManager.AddToRoleAsync(superUser, "Admin");
            }
        }
    }
}
