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
            if (this.db.Products.Any(dbProduct => dbProduct.ProductName.IgnoreCaseEquals(product.ProductName)))
                return;

            this.db.Products.Add(product);
            await this.db.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            //To prevent different products from having the same name
            if (db.Products.Any(dbProduct => dbProduct.ProductId != product.ProductId
                                                && dbProduct.ProductName.IgnoreCaseEquals(product.ProductName)))
                return;

            Product? dbProduct = await this.db.Products.FindAsync(product.ProductId);

            if (dbProduct != null)
            {
                dbProduct.ProductName = product.ProductName;
                dbProduct.Price = product.Price;
                dbProduct.Quantity = product.Quantity;
                dbProduct.ProductInventories = product.ProductInventories;

                await this.db.SaveChangesAsync();
            }
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await this.db.Products
                .Include(product => product.ProductInventories)
                .ThenInclude(productInventory => productInventory.Inventory)
                .FirstAsync(product => product.ProductId == productId);
        }
    }
}