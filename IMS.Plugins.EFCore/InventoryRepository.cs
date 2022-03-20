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

        public async Task<IEnumerable<Inventory>> GetInventoriesByName(string name)
        {
            return await this.db.Inventories
                .Where(inventory => inventory.InventoryName.Contains(name, StringComparison.CurrentCultureIgnoreCase) || string.IsNullOrWhiteSpace(name))
                .ToListAsync();
        }
    }
}