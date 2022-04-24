using System.ComponentModel.DataAnnotations;

namespace IMS.WebApp.ViewModels;

public class ProduceViewModel
{
    [Required]
    public string ProductionNumber { get; set; } = default!;

    [Required]
    public int ProductId { get; set; }

    [Required]
    public string ProductName { get; set; } = default!;

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater or equal to 1")]
    public int QuantityToProduce { get; set; }
}
