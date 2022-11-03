using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EFCore;

public class ProductRepository : IProductRepository
{
    private readonly IMSContext db;

    public ProductRepository(IMSContext db)
    {
        this.db = db;
    }

    public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
    {
        return await db.Products
            .Where(product => product.ProductName.IgnoreCaseContains(name) || string.IsNullOrWhiteSpace(name))
            .Include(product => product.ProductInventories)
            .ThenInclude(productInventory => productInventory.Inventory)
            .ToListAsync();
    }

    public async Task AddProductAsync(Product product)
    {
        //To prevent different products from having the same name
        if (db.Products.Any(dbProduct => dbProduct.ProductName.IgnoreCaseEquals(product.ProductName)))
            return;

        db.Products.Add(product);
        await db.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(Product product)
    {
        //To prevent different products from having the same name
        if (db.Products.Any(dbProduct => dbProduct.ProductId != product.ProductId
                                            && dbProduct.ProductName.IgnoreCaseEquals(product.ProductName)))
            return;

        var dbProduct = await db.Products.FindAsync(product.ProductId);

        if (dbProduct != null)
        {
            dbProduct.ProductName = product.ProductName;
            dbProduct.Price = product.Price;
            dbProduct.Quantity = product.Quantity;
            dbProduct.ProductInventories = product.ProductInventories;

            await db.SaveChangesAsync();
        }
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        return await db.Products
            .Include(product => product.ProductInventories)
            .ThenInclude(productInventory => productInventory.Inventory)
            .FirstAsync(product => product.ProductId == productId);
    }
}
