using System.ComponentModel.DataAnnotations;

namespace IMS.UseCases;

public enum InventoryTransactionSearchType
{
    [Display(Name = "All Activities")]
    AllActivities,

    [Display(Name = "Purchase Inventory")]
    PurchaseInventory,

    [Display(Name = "Produce Product")]
    ProduceProduct
}
