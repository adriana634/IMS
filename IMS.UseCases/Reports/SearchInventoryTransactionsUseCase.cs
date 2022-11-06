using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases;

public class SearchInventoryTransactionsUseCase : ISearchInventoryTransactionsUseCase
{
    private readonly IInventoryTransactionRepository inventoryTransactionRepository;

    public SearchInventoryTransactionsUseCase(IInventoryTransactionRepository inventoryTransactionRepository)
    {
        this.inventoryTransactionRepository = inventoryTransactionRepository;
    }

    public async Task<IReadOnlyList<InventoryTransaction>> ExecuteAsync(
            string? inventoryName,
            DateTime? dateFrom,
            DateTime? dateTo,
            InventoryTransactionSearchType searchType)
    {
        InventoryTransactionType? inventoryTransactionType = null;

        switch (searchType)
        {
            case InventoryTransactionSearchType.AllActivities:
                inventoryTransactionType = null;
                break;
            case InventoryTransactionSearchType.PurchaseInventory:
                inventoryTransactionType = InventoryTransactionType.PurchaseInventory;
                break;
            case InventoryTransactionSearchType.ProduceProduct:
                inventoryTransactionType = InventoryTransactionType.ProduceProduct;
                break;
        }

        return await inventoryTransactionRepository.GetInventoryTransactions(inventoryName, dateFrom, dateTo, inventoryTransactionType);
    }
}
