using System.ComponentModel.DataAnnotations;

namespace IMS.CoreBusiness;

public class InventoryTransaction 
{
    public int InventoryTransactionId { get; set; }

    [Required]
    public int InventoryId { get; set; }

    [Required]
    public InventoryTransactionType InventoryType { get; set; }

    [Required]
    public int QuantityBefore { get; set; }

    [Required]
    public int QuantityAfter { get; set; }

    public string? PurchaseOrderNumber { get; set; }
    public string? ProductionNumber { get; set; }

    public double? Cost { get; set; }

    [Required]
    public DateTime TransactionDate { get; set; }

    [Required]
    public string DoneBy { get; set; } = default!;

    public Inventory Inventory { get; set; } = default!;
}
