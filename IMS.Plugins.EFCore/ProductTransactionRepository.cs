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
        foreach (var productInventory in product.ProductInventories)
        {
            int quantityBefore = productInventory.Inventory.Quantity;

            productInventory.Inventory.Quantity -= quantity * productInventory.InventoryQuantity;

            var inventoryTransaction = new InventoryTransaction {
                ProductionNumber = productionNumber,
                InventoryId = productInventory.InventoryId,
                QuantityBefore = quantityBefore,
                QuantityAfter = productInventory.Inventory.Quantity,
                ActivityType = InventoryTransactionType.ProduceProduct,
                TransactionDate = DateTime.Now,
                DoneBy = doneBy,
                UnitPrice = product.Price
            };
            db.InventoryTransactions.Add(inventoryTransaction);
        }

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
        var productTransaction = new ProductTransaction {
            SalesOrderNumber = salesOrderNumber,
            ProductId = product.ProductId,
            QuantityBefore = product.Quantity,
            QuantityAfter = product.Quantity - quantity,
            ActivityType = ProductTransactionType.SellProduct,
            TransactionDate = DateTime.Now,
            DoneBy = doneBy,
            UnitPrice = price
        };
        db.ProductTransactions.Add(productTransaction);

        await db.SaveChangesAsync();
    }
}
