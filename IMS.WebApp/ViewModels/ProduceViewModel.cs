using System.ComponentModel.DataAnnotations;

namespace IMS.WebApp.ViewModels;

public sealed class ProduceViewModel
{
    [Required]
    public string ProductionNumber { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    public string ProductName { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater or equal to 1")]
    public int QuantityToProduce { get; set; }
}
