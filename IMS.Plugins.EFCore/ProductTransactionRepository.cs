using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.EFCore;

public class ProductTransactionRepository : IProductTransactionRepository
{
    private readonly IMSContext db;

    public ProductTransactionRepository(IMSContext db)
    {
        this.db = db;
    }

    public async Task ProduceAsync(string productionNumber, Product product, int quantity, string doneBy)
    {
        product.TakeAwayInventories(quantity);

        ProductTransaction productTransaction = new ProductTransaction 
        {
            ProductionNumber = productionNumber,
            ProductId = product.ProductId,
            QuantityBefore = product.Quantity,
            ActivityType = ProductTransactionType.ProduceProduct,
            QuantityAfter = product.Quantity + quantity,
            TransactionDate = DateTime.Now,
            DoneBy = doneBy,
            UnitPrice = product.Price
        };

        this.db.ProductTransactions.Add(productTransaction);
        await this.db.SaveChangesAsync();
    }
}
