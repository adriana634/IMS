using System.ComponentModel.DataAnnotations;

namespace IMS.WebApp.ViewModels;

public sealed class PurchaseViewModel
{
    [Required]
    public string PurchaseOrderNumber { get; set; }

    [Required]
    public int InventoryId { get; set; }

    [Required]
    public string InventoryName { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater or equal to 1")]
    public int QuantityToPurchase { get; set; }
}
