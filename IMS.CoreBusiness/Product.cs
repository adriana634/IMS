using IMS.CoreBusiness.Validations;
using System.ComponentModel.DataAnnotations;

namespace IMS.CoreBusiness;

public class Product
{
    public int ProductId { get; set; }

    [Required]
    public string ProductName { get; set; } = default!;

    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater or equal to 0")]
    public int Quantity { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Price must be greater or equal to 0")]
    [Product_EnsurePriceIsGreaterThanInventoriesPrice]
    public double Price { get; set; }

    public ICollection<ProductInventory> ProductInventories { get; set; } = default!;

    public double TotalInventoryCost => this.ProductInventories
                                        .Sum(ProductInventory => ProductInventory.Inventory.Price * ProductInventory.InventoryQuantity);

    public bool ValidatePricing()
    {
        if (ProductInventories.Count == 0) return true;
        if (this.TotalInventoryCost > this.Price) return false;
        return true;
    }

    public bool ValidateEnoughInventoriesForProducing(int quantity)
    {
        if (this.ProductInventories.Any(productInventory => productInventory.InventoryQuantity * quantity > productInventory.Inventory.Quantity))
            return false;

        return true;
    }

    public void TakeAwayInventories(int quantity)
    {
        foreach (ProductInventory productInventory in this.ProductInventories)
        {
            productInventory.Inventory.Quantity -= quantity * productInventory.InventoryQuantity;
        }
    }
}
