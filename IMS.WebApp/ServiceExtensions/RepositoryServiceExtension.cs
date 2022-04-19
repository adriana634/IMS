using IMS.Plugins.EFCore;
using IMS.UseCases.PluginInterfaces;

namespace IMS.WebApp.ServiceExtensions;

internal static class RepositoryServiceExtension
{
    internal static IServiceCollection AddRepositoryServices(this IServiceCollection services)
    {
        services
            .AddTransient<IInventoryRepository, InventoryRepository>()
            .AddTransient<IProductRepository, ProductRepository>()
            .AddTransient<IInventoryTransactionRepository, InventoryTransactionRepository>();
        
        return services;
    } 
}
