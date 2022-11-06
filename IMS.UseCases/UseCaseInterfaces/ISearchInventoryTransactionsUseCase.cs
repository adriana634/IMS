using IMS.CoreBusiness;

namespace IMS.UseCases;

public interface ISearchInventoryTransactionsUseCase
{
    Task<IReadOnlyList<InventoryTransaction>> ExecuteAsync(string? inventoryName, DateTime? dateFrom, DateTime? dateTo, InventoryTransactionSearchType searchType);
}