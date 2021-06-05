using HotelService.Infrastructure;
using HotelService.Models;
using HotelService.Models.Base;
using HotelService.Repositories;
using HotelService.Repositories.Interfaces;
using HotelService.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HotelService
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
            Configuration.Bind("ConnectionStrings", new Config());
            Configuration.Bind("Developer", new Developer());

            //services.AddTransient<IPasswordValidator<AppUser>,
            //    CustomPasswordValidator>();
            //services.AddTransient<IUserValidator<AppUser>,
            //    CustomUserValidator>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<HotelServiceContext>(options => options.UseSqlServer(Config.DefaultConnection));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<User, Role>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 5;
            }).AddDefaultTokenProviders()
              .AddDefaultUI()
              .AddEntityFrameworkStores<HotelServiceContext>();

            services.AddScoped<ICatalogManager, CatalogManager>();

            services.AddCoreAdmin("Admin", "Developer");
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddSession();
            services.AddRazorPages();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseRequestLocalization(new RequestLocalizationOptions
            //{
            //    DefaultRequestCulture = new RequestCulture("en-US"),
            //});

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseStatusCodePages();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // маршрут для области Admin
                endpoints.MapAreaControllerRoute(
                    name: "Admin_Area",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

                // маршрут для области Client
                endpoints.MapAreaControllerRoute(
                    name: "Client_Area",
                    areaName: "Client",
                    pattern: "Client/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
