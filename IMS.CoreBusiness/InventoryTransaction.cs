using System.ComponentModel.DataAnnotations;

namespace IMS.CoreBusiness;

public sealed class InventoryTransaction 
{
    public int InventoryTransactionId { get; set; }

    [Required]
    public int InventoryId { get; set; }

    [Required]
    public InventoryTransactionType ActivityType { get; set; }

    [Required]
    public int QuantityBefore { get; set; }

    [Required]
    public int QuantityAfter { get; set; }

    public string? PurchaseOrderNumber { get; set; }
    public string? ProductionNumber { get; set; }

    public double? UnitPrice { get; set; }

    [Required]
    public DateTime TransactionDate { get; set; }

    [Required]
    public string DoneBy { get; set; }

    public Inventory Inventory { get; set; }
}
