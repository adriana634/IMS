using System.ComponentModel.DataAnnotations;

namespace IMS.WebApp.Pages.Activities;

public enum InventoryTransactionSearchType
{
    [Display(Name = "All Activities")]
    AllActivities,

    [Display(Name = "Purchase Inventory")]
    PurchaseInventory,

    [Display(Name = "Produce Product")]
    ProduceProduct
}
