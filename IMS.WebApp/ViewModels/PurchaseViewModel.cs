using System.ComponentModel.DataAnnotations;

namespace IMS.WebApp.ViewModels;

public class PurchaseViewModel
{
    [Required]
    public string PurchaseOrderNumber { get; set; } = default!;

    [Required]
    public int InventoryId { get; set; }

    [Required]
    public string InventoryName { get; set; } = default!;

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater or equal to 1")]
    public int QuantityToPurchase { get; set; }

    [Required]
    public double InventoryPrice { get; set; }
}
