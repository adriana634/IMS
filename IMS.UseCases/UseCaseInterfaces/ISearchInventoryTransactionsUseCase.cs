using IMS.CoreBusiness;

namespace IMS.UseCases;

public interface ISearchInventoryTransactionsUseCase
{
    Task<IReadOnlyList<InventoryTransaction>> ExecuteAsync(string? inventoryName, DateOnly? dateFrom, DateOnly? dateTo, InventoryTransactionSearchType searchType);
}