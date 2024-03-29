﻿using IMS.CoreBusiness;

namespace IMS.UseCases.PluginInterfaces;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProductsByNameAsync(string name);
    Task AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task<Product?> GetProductByIdAsync(int productId);
}
