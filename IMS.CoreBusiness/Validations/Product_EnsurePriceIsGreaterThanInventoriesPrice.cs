using System.ComponentModel.DataAnnotations;

namespace IMS.CoreBusiness.Validations;

internal class Product_EnsurePriceIsGreaterThanInventoriesPrice : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        Product? product = validationContext.ObjectInstance as Product;
        if (product != null)
        {
            bool isPricingValid = product.ValidatePricing();
            if (isPricingValid == false)
            {
                string[] memberNames = new string[] { validationContext.MemberName! };
                return new ValidationResult(
                    $"The product's price is less than the summary of its inventories' price: { product.TotalInventoryCost }",
                    memberNames);
            }
        }

        return ValidationResult.Success;
    }
}
