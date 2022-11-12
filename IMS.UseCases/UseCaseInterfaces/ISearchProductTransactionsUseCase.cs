using IMS.CoreBusiness;

namespace IMS.UseCases;

public interface ISearchProductTransactionsUseCase
{
    Task<IReadOnlyList<ProductTransaction>> ExecuteAsync(string? productName, DateOnly? dateFrom, DateOnly? dateTo, ProductTransactionSearchType searchType);
}