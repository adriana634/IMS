using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EFCore
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMSContext db;

        public ProductRepository(IMSContext db)
        {
            this.db = db;
        }

        public async Task<List<Product>> GetProductsByNameAsync(string name)
        {
            return await this.db.Products
                .Where(product => product.ProductName.Contains(name, StringComparison.CurrentCultureIgnoreCase) || string.IsNullOrWhiteSpace(name))
                .ToListAsync();
        }
    }
}