using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EFCore;

public sealed class InventoryTransactionRepository : IInventoryTransactionRepository
{
    private readonly IMSContext db;

    public InventoryTransactionRepository(IMSContext db)
    {
        this.db = db;
    }

    public async Task PurchaseAsync(
        string purchaseOrderNumber,
        int inventoryId,
        int inventoryQuantity,
        int purchaseQuantity,
        double inventoryPrice,
        string doneBy)
    {
        var inventoryTransaction = new InventoryTransaction {
            PurchaseOrderNumber = purchaseOrderNumber,
            InventoryId = inventoryId,
            QuantityBefore = inventoryQuantity,
            QuantityAfter = inventoryQuantity + purchaseQuantity,
            ActivityType = InventoryTransactionType.PurchaseInventory,
            TransactionDate = DateTime.Now,
            DoneBy = doneBy,
            UnitPrice = inventoryPrice
        };

        db.InventoryTransactions.Add(inventoryTransaction);
        await db.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<InventoryTransaction>> GetInventoryTransactions(
        string? inventoryName,
        DateOnly? dateFrom,
        DateOnly? dateTo,
        InventoryTransactionType? activityType)
    {
        var query = from it in db.InventoryTransactions
                    join inv in db.Inventories on it.InventoryId equals inv.InventoryId
                    where
                        (string.IsNullOrEmpty(inventoryName) || EF.Functions.Like(inv.InventoryName, "%" + inventoryName + "%"))
                        && (dateFrom.HasValue == false || DateOnly.FromDateTime(it.TransactionDate) >= dateFrom)
                        && (dateTo.HasValue == false || DateOnly.FromDateTime(it.TransactionDate) <= dateTo)
                        && (activityType.HasValue == false || it.ActivityType == activityType)
                    select it;

        var inventoryTransactions = await query.AsNoTracking()
                                               .Include(inventoryTransaction => inventoryTransaction.Inventory)
                                               .ToListAsync();

        return inventoryTransactions.AsReadOnly();
    }
}
