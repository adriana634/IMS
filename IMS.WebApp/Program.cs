using IMS.Plugins.EFCore;
using IMS.WebApp.ServiceExtensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddMainServices(builder.Configuration)
    .AddRepositoryServices()
    .AddUseCaseServices();

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
