using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EFCore
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IMSContext _db;
        public InventoryRepository(IMSContext db)
        {
            this._db = db;
        }

        public async Task<IEnumerable<Inventory>> GetInventoriesByName(string name)
        {
            return await this._db.Inventories
                .Where(inventory => inventory.InventoryName.Contains(name) || string.IsNullOrWhiteSpace(name))
                .ToListAsync();
        }
    }
}