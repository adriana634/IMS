using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases;

public class EditInventoryUseCase : IEditInventoryUseCase
{
    private readonly IInventoryRepository inventoryRepository;

    public EditInventoryUseCase(IInventoryRepository inventoryRepository)
    {
        this.inventoryRepository = inventoryRepository;
    }

    public async Task ExecuteAsync(Inventory inventory)
    {
        if (inventory == null) return;

        await this.inventoryRepository.UpdateInventoryAsync(inventory);
    }
}
