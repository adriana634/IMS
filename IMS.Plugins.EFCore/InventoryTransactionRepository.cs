using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.EFCore;

public class InventoryTransactionRepository : IInventoryTransactionRepository
{
    private readonly IMSContext db;

    public InventoryTransactionRepository(IMSContext db)
    {
        this.db = db;
    }

    public async Task PurchaseAsync(string purchaseOrderNumber,
                                    int inventoryId,
                                    int inventoryQuantity,
                                    int purchaseQuantity,
                                    double inventoryPrice,
                                    string doneBy)
    {
        InventoryTransaction inventoryTransaction = new InventoryTransaction {
            PurchaseOrderNumber = purchaseOrderNumber,
            InventoryId = inventoryId,
            QuantityBefore = inventoryQuantity,
            InventoryType = InventoryTransactionType.PurchaseInventory,
            QuantityAfter = inventoryQuantity + purchaseQuantity,
            TransactionDate = DateTime.Now,
            DoneBy = doneBy,
            Cost = inventoryPrice * purchaseQuantity
        };

        this.db.InventoryTransactions.Add(inventoryTransaction);
        await this.db.SaveChangesAsync();
    }
}
