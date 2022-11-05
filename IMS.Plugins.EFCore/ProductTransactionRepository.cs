using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.EFCore;

public sealed class ProductTransactionRepository : IProductTransactionRepository
{
    private readonly IMSContext db;

    public ProductTransactionRepository(IMSContext db)
    {
        this.db = db;
    }

    public async Task ProduceAsync(
        string productionNumber,
        Product product,
        int quantity,
        string doneBy)
    {
        product.TakeAwayInventories(quantity);

        var productTransaction = new ProductTransaction 
        {
            ProductionNumber = productionNumber,
            ProductId = product.ProductId,
            QuantityBefore = product.Quantity,
            QuantityAfter = product.Quantity + quantity,
            ActivityType = ProductTransactionType.ProduceProduct,
            TransactionDate = DateTime.Now,
            DoneBy = doneBy,
            UnitPrice = product.Price
        };

        db.ProductTransactions.Add(productTransaction);
        await db.SaveChangesAsync();
    }

    public async Task SellProductAsync(
        string salesOrderNumber,
        Product product,
        int quantity,
        double price,
        string doneBy)
    {
        db.ProductTransactions.Add(new ProductTransaction 
        {
            SalesOrderNumber = salesOrderNumber,
            ProductId = product.ProductId,
            QuantityBefore = product.Quantity,
            QuantityAfter = product.Quantity - quantity,
            ActivityType = ProductTransactionType.SellProduct,
            TransactionDate = DateTime.Now,
            DoneBy = doneBy,
            UnitPrice = price
        });
        await db.SaveChangesAsync();
    }
}
