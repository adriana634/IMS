using AutoMapper;
using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases;

public class SearchProductTransactionsUseCase : ISearchProductTransactionsUseCase
{
    private readonly IProductTransactionRepository productTransactionRepository;
    private readonly IMapper mapper;

    public SearchProductTransactionsUseCase(IProductTransactionRepository productTransactionRepository, IMapper mapper)
    {
        this.productTransactionRepository = productTransactionRepository;
        this.mapper = mapper;
    }

    public async Task<IReadOnlyList<ProductTransaction>> ExecuteAsync(
            string? productName,
            DateOnly? dateFrom,
            DateOnly? dateTo,
            ProductTransactionSearchType searchType)
    {
        var activityType = mapper.Map<ProductTransactionType?>(searchType);
        return await productTransactionRepository.GetProductTransactionsAsync(productName, dateFrom, dateTo, activityType);
    }
}
