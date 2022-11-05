using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases;

public sealed class ProduceProductUseCase : IProduceProductUseCase
{
    private readonly IInventoryRepository inventoryRepository;
    private readonly IProductRepository productRepository;
    private readonly IInventoryTransactionRepository inventoryTransactionRepository;
    private readonly IProductTransactionRepository productTransactionRepository;

    public ProduceProductUseCase(
        IInventoryRepository inventoryRepository,
        IProductRepository productRepository,
        IInventoryTransactionRepository inventoryTransactionRepository,
        IProductTransactionRepository productTransactionRepository)
    {
        this.inventoryRepository = inventoryRepository;
        this.productRepository = productRepository;
        this.inventoryTransactionRepository = inventoryTransactionRepository;
        this.productTransactionRepository = productTransactionRepository;
    }

    public async Task ExecuteAsync(string productionNumber, Product product, int quantity, string doneBy)
    {
        await productTransactionRepository.ProduceAsync(productionNumber, product, quantity, doneBy);

        product.Quantity += quantity;
        await productRepository.UpdateProductAsync(product);
    }
}
