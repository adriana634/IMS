using System.ComponentModel.DataAnnotations;

namespace IMS.CoreBusiness;

public sealed class Inventory
{
    public int InventoryId { get; set; }

    [Required]
    public string InventoryName { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater or equal to 0")]
    public int Quantity { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be greater or equal to 0")]
    public double Price { get; set; }

    public ICollection<ProductInventory> ProductInventories { get; set; }
}
