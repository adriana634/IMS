﻿using IMS.CoreBusiness;

namespace IMS.UseCases.PluginInterfaces;

public interface IProductTransactionRepository
{
    Task ProduceAsync(string productionNumber, Product product, int quantity, string doneBy);
    Task SellProductAsync(string salesOrderNumber, Product product, int quantity, double price, string doneBy);
    Task<IReadOnlyList<ProductTransaction>> GetProductTransactionsAsync(string? productName, DateOnly? dateFrom, DateOnly? dateTo, ProductTransactionType? activityType);
}
