using IMS.CoreBusiness;

namespace IMS.UseCases;

public interface IPurchaseInventoryUseCase
{
    Task ExecuteAsync(string purchaseOrderNumber,
                      Inventory inventory,
                      int quantity,
                      string doneBy);
}