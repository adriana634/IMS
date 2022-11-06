using IMS.CoreBusiness.Validations;
using System.ComponentModel.DataAnnotations;

namespace IMS.CoreBusiness;

public sealed class Product
{
    public int ProductId { get; set; }

    [Required]
    public string ProductName { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater or equal to 0")]
    public int Quantity { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be greater or equal to 0")]
    [Product_EnsurePriceIsGreaterThanInventoriesPrice]
    public double Price { get; set; }

    public ICollection<ProductInventory> ProductInventories { get; set; }

    public double TotalInventoryCost => ProductInventories
                                        .Sum(ProductInventory => ProductInventory.Inventory.Price * ProductInventory.InventoryQuantity);

    public bool ValidatePricing()
    {
        if (ProductInventories.Count == 0) return true;
        if (TotalInventoryCost > Price) return false;
        return true;
    }

    public bool ValidateEnoughInventoriesForProducing(int quantity)
    {
        bool inventoriesAreNotEnough = ProductInventories
            .Any(productInventory => productInventory.InventoryQuantity * quantity > productInventory.Inventory.Quantity);

        if (inventoriesAreNotEnough)
            return false;

        return true;
    }

    public bool ValidateEnoughQuantityForSelling(int quantity)
    {
        if (quantity > Quantity) return false;
        return true;
    }
}
