using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EFCore;

public sealed class InventoryRepository : IInventoryRepository
{
    private readonly IMSContext db;

    public InventoryRepository(IMSContext db)
    {
        this.db = db;
    }

    public async Task<IReadOnlyList<Inventory>> GetInventoriesByNameAsync(string name)
    {
        var inventories = await db.Inventories
            .Where(inventory => inventory.InventoryName.IgnoreCaseContains(name) || string.IsNullOrWhiteSpace(name))
            .Include(product => product.ProductInventories)
            .AsNoTracking()
            .ToListAsync();

        return inventories.AsReadOnly(); 
    }

    public async Task AddInventoryAsync(Inventory inventory)
    {
        // To prevent different inventories from having the same name
        if (db.Inventories.Any(dbInventory => dbInventory.InventoryName.IgnoreCaseEquals(inventory.InventoryName)))
            return;

        await db.Inventories.AddAsync(inventory);
        await db.SaveChangesAsync();
    }

    public async Task UpdateInventoryAsync(Inventory inventory)
    {
        // To prevent different inventories from having the same name
        if (db.Inventories.Any(dbInventory => dbInventory.InventoryId != inventory.InventoryId 
                                            && dbInventory.InventoryName.IgnoreCaseEquals(inventory.InventoryName)))
            return;

        var dbInventory = await db.Inventories.FindAsync(inventory.InventoryId);
        
        if (dbInventory != null)
        {
            dbInventory.InventoryName = inventory.InventoryName;
            dbInventory.Price = inventory.Price;
            dbInventory.Quantity = inventory.Quantity;

            await db.SaveChangesAsync();
        }
    }

    public async Task<Inventory?> GetInventoryByIdAsync(int inventoryId)
    {
        return await db.Inventories.FindAsync(inventoryId);
    }
}
