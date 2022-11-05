using System.ComponentModel.DataAnnotations;

namespace IMS.WebApp.ViewModels;

public sealed class SellViewModel
{
    [Required]
    public string SalesOrderNumber { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    public string ProductName { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater or equal to 1")]
    public int QuantityToSell { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be greater or equal to 0")]
    public double ProductPrice { get; set; }
}
