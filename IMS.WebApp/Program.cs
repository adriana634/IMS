using IMS.Plugins.EFCore;
using IMS.UseCases;
using IMS.UseCases.PluginInterfaces;
using IMS.WebApp.Areas.Identity;
using IMS.WebApp.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddDbContext<IMSContext>(options =>
{
    options.UseInMemoryDatabase("IMS");
});

//DI repositories
builder.Services
    .AddTransient<IInventoryRepository, InventoryRepository>()
    .AddTransient<IProductRepository, ProductRepository>();

//DI use cases
builder.Services
    .AddTransient<IViewInventoriesByNameUseCase, ViewInventoriesByNameUseCase>()
    .AddTransient<IAddInventoryUseCase, AddInventoryUseCase>()
    .AddTransient<IEditInventoryUseCase, EditInventoryUseCase>()
    .AddTransient<IViewInventoryByIdUseCase, ViewInventoryByIdUseCase>()
    .AddTransient<IViewProductsByNameUseCase, ViewProductsUseCase>();

WebApplication app = builder.Build();

IServiceScope scope = app.Services.CreateScope();
IMSContext imsContext = scope.ServiceProvider.GetRequiredService<IMSContext>();
imsContext.Database.EnsureDeleted();
imsContext.Database.EnsureCreated();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
