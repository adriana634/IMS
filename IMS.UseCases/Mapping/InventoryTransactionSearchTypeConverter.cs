using AutoMapper;
using IMS.CoreBusiness;

namespace IMS.UseCases.Mapping;

public class InventoryTransactionSearchTypeConverter : ITypeConverter<InventoryTransactionSearchType, InventoryTransactionType?>
{
    public InventoryTransactionType? Convert(InventoryTransactionSearchType source, InventoryTransactionType? destination, ResolutionContext context)
    {
        return source switch {
            InventoryTransactionSearchType.AllActivities => null,
            InventoryTransactionSearchType.PurchaseInventory => InventoryTransactionType.PurchaseInventory,
            InventoryTransactionSearchType.ProduceProduct => InventoryTransactionType.ProduceProduct,
            _ => throw new InvalidCastException(),
        };
    }
}
