using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases;

public class ProduceProductUseCase : IProduceProductUseCase
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

    public async Task ExecuteAync(string productionNumber, Product product, int quantity, double price, string doneBy)
    {
        await this.productTransactionRepository.ProduceAsync(productionNumber, product, quantity, price, doneBy);

        product.Quantity += quantity;
        await this.productRepository.UpdateProductAsync(product);
    }
}
