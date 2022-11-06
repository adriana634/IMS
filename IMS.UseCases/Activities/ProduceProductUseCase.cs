using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases;

public sealed class ProduceProductUseCase : IProduceProductUseCase
{
    private readonly IProductRepository productRepository;
    private readonly IProductTransactionRepository productTransactionRepository;

    public ProduceProductUseCase(
        IProductRepository productRepository,
        IProductTransactionRepository productTransactionRepository)
    {
        this.productRepository = productRepository;
        this.productTransactionRepository = productTransactionRepository;
    }

    public async Task ExecuteAsync(string productionNumber, Product product, int quantity, string doneBy)
    {
        //TODO: Handle Transaction

        try
        {
            await productTransactionRepository.ProduceAsync(productionNumber, product, quantity, doneBy);

            product.Quantity += quantity;
            await productRepository.UpdateProductAsync(product);
        }
        catch (Exception ex)
        {
            //TODO: Log exception
            throw;
        }
    }
}
