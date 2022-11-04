using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases;

public sealed class PurchaseInventoryUseCase : IPurchaseInventoryUseCase
{
    private readonly IInventoryTransactionRepository inventoryTransactionRepository;
    private readonly IInventoryRepository inventoryRepository;

    public PurchaseInventoryUseCase(IInventoryTransactionRepository inventoryTransactionRepository, IInventoryRepository inventoryRepository)
    {
        this.inventoryTransactionRepository = inventoryTransactionRepository;
        this.inventoryRepository = inventoryRepository;
    }

    public async Task ExecuteAsync(string purchaseOrderNumber,
                                   Inventory inventory,
                                   int quantity,
                                   string doneBy)
    {
        await inventoryTransactionRepository.PurchaseAsync(
            purchaseOrderNumber,
            inventory.InventoryId,
            inventory.Quantity,
            quantity,
            inventory.Price,
            doneBy);

        inventory.Quantity += quantity;
        await inventoryRepository.UpdateInventoryAsync(inventory);
    }
}
