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
                .Where(product => product.ProductName.IgnoreCaseContains(name) || string.IsNullOrWhiteSpace(name))
                .ToListAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            //To prevent different products from having the same name
            if (this.db.Inventories.Any(dbInventory => dbInventory.InventoryName.IgnoreCaseEquals(product.ProductName)))
                return;

            this.db.Products.Add(product);
        }
    }
}