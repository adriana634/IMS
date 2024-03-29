﻿using IMS.CoreBusiness;

namespace IMS.UseCases.PluginInterfaces;

public interface IInventoryTransactionRepository
{    
    Task PurchaseAsync(string purchaseOrderNumber, int inventoryId, int inventoryQuantity, int purchaseQuantity, double inventoryPrice, string doneBy);
    Task<IReadOnlyList<InventoryTransaction>> GetInventoryTransactions(string? inventoryName, DateOnly? dateFrom, DateOnly? dateTo, InventoryTransactionType? activityType);
}