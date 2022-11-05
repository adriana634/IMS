using IMS.Plugins.EFCore;
using IMS.UseCases.PluginInterfaces;

namespace IMS.WebApp.ServiceExtensions;

internal static class RepositoryServiceExtension
{
    internal static IServiceCollection AddDataAccessServices(this IServiceCollection services)
    {
        services
            .AddTransient<IInventoryRepository, InventoryRepository>()
            .AddTransient<IProductRepository, ProductRepository>()
            .AddTransient<IInventoryTransactionRepository, InventoryTransactionRepository>()
            .AddTransient<IProductTransactionRepository, ProductTransactionRepository>();

        services
            .AddTransient<IProduceService, ProduceService>()
            .AddTransient<ISellService, SellService>();

        return services;
    } 
}
