using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases;

public sealed class ViewProductsUseCase : IViewProductsByNameUseCase
{
    private readonly IProductRepository productRepository;

    public ViewProductsUseCase(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<IReadOnlyList<Product>> ExecuteAsync(string name = "")
    {
        return await productRepository.GetProductsByNameAsync(name);
    }
}
