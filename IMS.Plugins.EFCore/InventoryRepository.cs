using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EFCore
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IMSContext db;

        public InventoryRepository(IMSContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name)
        {
            return await this.db.Inventories
                .Where(inventory => inventory.InventoryName.IgnoreCaseContains(name) || string.IsNullOrWhiteSpace(name))
                .Include(product => product.ProductInventories)
                .ToListAsync();
        }

        public async Task AddInventoryAsync(Inventory inventory)
        {
            //To prevent different inventories from having the same name
            if (this.db.Inventories.Any(dbInventory => dbInventory.InventoryName.IgnoreCaseEquals(inventory.InventoryName)))
                return;

            await this.db.Inventories.AddAsync(inventory);
            await this.db.SaveChangesAsync();
        }

        public async Task UpdateInventoryAsync(Inventory inventory)
        {
            //To prevent different inventories from having the same name
            if (db.Inventories.Any(dbInventory => dbInventory.InventoryId != inventory.InventoryId 
                                                && dbInventory.InventoryName.IgnoreCaseEquals(inventory.InventoryName)))
                return;

            Inventory? dbInventory = await this.db.Inventories.FindAsync(inventory.InventoryId);
            
            if (dbInventory != null)
            {
                dbInventory.InventoryName = inventory.InventoryName;
                dbInventory.Price = inventory.Price;
                dbInventory.Quantity = inventory.Quantity;

                await this.db.SaveChangesAsync();
            }
        }

        public async Task<Inventory?> GetInventoryByIdAsync(int inventoryId)
        {
            return await this.db.Inventories.FindAsync(inventoryId);
        }
    }
}