using AutoMapper;
using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases;

public class SearchInventoryTransactionsUseCase : ISearchInventoryTransactionsUseCase
{
    private readonly IInventoryTransactionRepository inventoryTransactionRepository;
    private readonly IMapper mapper;

    public SearchInventoryTransactionsUseCase(IInventoryTransactionRepository inventoryTransactionRepository, IMapper mapper)
    {
        this.inventoryTransactionRepository = inventoryTransactionRepository;
        this.mapper = mapper;
    }

    public async Task<IReadOnlyList<InventoryTransaction>> ExecuteAsync(
            string? inventoryName,
            DateOnly? dateFrom,
            DateOnly? dateTo,
            InventoryTransactionSearchType searchType)
    {
        var inventoryTransactionType = mapper.Map<InventoryTransactionType?>(searchType);
        return await inventoryTransactionRepository.GetInventoryTransactions(inventoryName, dateFrom, dateTo, inventoryTransactionType);
    }
}
