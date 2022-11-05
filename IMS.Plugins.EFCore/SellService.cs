using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.EFCore;

public sealed class SellService : ISellService
{
    private readonly IMSContext db;

    private readonly IProductTransactionRepository productTransactionRepository;
    private readonly IProductRepository productRepository;

    public SellService(IMSContext db, IProductTransactionRepository productTransactionRepository, IProductRepository productRepository)
    {
        this.db = db;
        this.productTransactionRepository = productTransactionRepository;
        this.productRepository = productRepository;
    }

    public async Task SellProduct(string salesOrderNumber, Product product, int quantity, string doneBy)
    {
        //SQL Server
        //using var transaction = db.Database.BeginTransaction(); 

        try
        {
            await productTransactionRepository.SellProductAsync(salesOrderNumber, product, quantity, product.Price, doneBy);

            product.Quantity -= quantity;
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
