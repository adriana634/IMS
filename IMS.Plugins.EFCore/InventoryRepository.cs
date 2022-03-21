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
                .Where(inventory => inventory.InventoryName.Contains(name, StringComparison.CurrentCultureIgnoreCase) || string.IsNullOrWhiteSpace(name))
                .ToListAsync();
        }

        public async Task AddInventoryAsync(Inventory inventory)
        {
            await this.db.Inventories.AddAsync(inventory);
            await this.db.SaveChangesAsync();
        }
    }
}