using System.ComponentModel.DataAnnotations;

namespace IMS.CoreBusiness;

public class ProductInventory
{
    [Required]
    public int ProductId { get; set; }
    [Required]
    public Product Product { get; set; } = default!;

    [Required]
    public int InventoryId { get; set; }
    [Required]
    public Inventory Inventory { get; set; } = default!;

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater or equal to 1")]
    public int InventoryQuantity { get; set; }
}
