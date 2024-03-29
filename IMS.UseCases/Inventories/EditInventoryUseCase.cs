﻿using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases;

public sealed class EditInventoryUseCase : IEditInventoryUseCase
{
    private readonly IInventoryRepository inventoryRepository;

    public EditInventoryUseCase(IInventoryRepository inventoryRepository)
    {
        this.inventoryRepository = inventoryRepository;
    }

    public async Task ExecuteAsync(Inventory inventory)
    {
        if (inventory == null) return;

        await inventoryRepository.UpdateInventoryAsync(inventory);
    }
}
