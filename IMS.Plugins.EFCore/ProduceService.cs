using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.EFCore;

public sealed class ProduceService : IProduceService
{
    private readonly IMSContext db;

    private readonly IProductTransactionRepository productTransactionRepository;
    private readonly IProductRepository productRepository;

    public ProduceService(IMSContext db, IProductTransactionRepository productTransactionRepository, IProductRepository productRepository)
    {
        this.db = db;
        this.productTransactionRepository = productTransactionRepository;
        this.productRepository = productRepository;
    }

    public async Task ProduceProduct(string productionNumber, Product product, int quantity, string doneBy)
    {
        //SQL Server
        //using var transaction = db.Database.BeginTransaction();

        try
        {
            await productTransactionRepository.ProduceAsync(productionNumber, product, quantity, doneBy);

            product.Quantity += quantity;
            await productRepository.UpdateProductAsync(product);

            //SQL Server
            //await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            //TODO: Log exception
            throw;
        }
    }
}
