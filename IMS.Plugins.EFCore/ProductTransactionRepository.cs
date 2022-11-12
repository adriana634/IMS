using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IReadOnlyList<ProductTransaction>> GetProductTransactionsAsync(
        string? productName,
        DateOnly? dateFrom,
        DateOnly? dateTo,
        ProductTransactionType? activityType)
    {
        var pt2 = db.ProductTransactions.ToList();

        var query = from pt in db.ProductTransactions
                    join prod in db.Products on pt.ProductId equals prod.ProductId
                    where
                        (string.IsNullOrEmpty(productName) || EF.Functions.Like(prod.ProductName, "%" + productName + "%"))
                        && (dateFrom.HasValue == false || DateOnly.FromDateTime(pt.TransactionDate) >= dateFrom)
                        && (dateTo.HasValue == false || DateOnly.FromDateTime(pt.TransactionDate) <= dateTo)
                        && (activityType.HasValue == false || pt.ActivityType == activityType)
                    select pt;

        var productTransactions = await query.AsNoTracking()
                                               .Include(productTransaction => productTransaction.Product)
                                               .ToListAsync();

        return productTransactions.AsReadOnly();
    }
}
