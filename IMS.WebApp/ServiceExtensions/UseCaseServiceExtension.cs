using IMS.UseCases;

namespace IMS.WebApp.ServiceExtensions;

internal static class UseCaseExtension
{
    internal static IServiceCollection AddUseCaseServices(this IServiceCollection services)
    {
        services
            .AddTransient<IViewInventoriesByNameUseCase, ViewInventoriesByNameUseCase>()
            .AddTransient<IAddInventoryUseCase, AddInventoryUseCase>()
            .AddTransient<IEditInventoryUseCase, EditInventoryUseCase>()
            .AddTransient<IViewInventoryByIdUseCase, ViewInventoryByIdUseCase>()
            .AddTransient<IViewProductsByNameUseCase, ViewProductsUseCase>()
            .AddTransient<IAddProductUseCase, AddProductUseCase>()
            .AddTransient<IViewProductByIdUseCase, ViewProductByIdUseCase>()
            .AddTransient<IEditProductUseCase, EditProductUseCase>()
            .AddTransient<IPurchaseInventoryUseCase, PurchaseInventoryUseCase>();

        return services;
    } 
}
