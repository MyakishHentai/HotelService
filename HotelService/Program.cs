using HotelService.Models;
using HotelService.Models.Base;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HotelService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var Host = CreateHostBuilder(args).Build();

            using (var Scope = Host.Services.CreateScope())
            {
                var Services = Scope.ServiceProvider;
                try
                {
                    var UserManager = Services.GetRequiredService<UserManager<User>>();
                    var RolesManager = Services.GetRequiredService<RoleManager<Role>>();
                    await DeveloperInitializer.InitializeAsync(UserManager, RolesManager);
                }
                catch (Exception Ex)
                {
                    var Logger = Services.GetRequiredService<ILogger<Program>>();
                    Logger.LogError(Ex, "An error occurred while seeding the database.");
                }
            }

            await Host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseDefaultServiceProvider(opt => opt.ValidateScopes = false);
                });
    }
}
