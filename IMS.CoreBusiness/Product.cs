using IMS.CoreBusiness.Validations;
using System.ComponentModel.DataAnnotations;

namespace IMS.CoreBusiness
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        public string? ProductName { get; set; }
        
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater or equal to 0")]
        public int Quantity { get; set; }
        
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater or equal to 0")]
        [Product_EnsurePriceIsGreaterThanInventoriesPrice]
        public double Price { get; set; }

        public List<ProductInventory>? ProductInventories { get; set; }

        public double TotalInventoryCost()
        {
            double totalInventoryCost = this.ProductInventories?
                .Sum(ProductInventory => ProductInventory.Inventory?.Price * ProductInventory.InventoryQuantity ?? 0) ?? 0;

            return totalInventoryCost;
        }

        public bool ValidatePricing()
        {
            if (ProductInventories == null || ProductInventories.Count == 0) return true;

            double totalInventoryCost = this.TotalInventoryCost();
            if (totalInventoryCost > this.Price) return false;

            return true;
        }
    }
}
