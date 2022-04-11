using System.ComponentModel.DataAnnotations;

namespace IMS.CoreBusiness
{
    public class Inventory
    {
        public int InventoryId { get; set; }

        public string InventoryName { get; set; } = default!;
        
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater or equal to 0")]
        public int Quantity { get; set; }
        
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater or equal to 0")]
        public double Price { get; set; }

        public List<ProductInventory> ProductInventories { get; set; } = default!;
    }
}