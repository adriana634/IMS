using IMS.Plugins.EFCore;
using IMS.WebApp.Areas.Identity;
using IMS.WebApp.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace IMS.WebApp.ServiceExtensions;

internal static class MainServiceExtension
{
    internal static IServiceCollection AddMainServices(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddRazorPages();
        services.AddServerSideBlazor();
        services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
        services.AddSingleton<WeatherForecastService>();

        services.AddDbContext<IMSContext>(options =>
        {
            options.UseInMemoryDatabase("IMS");
        });

        return services;
    } 
}
