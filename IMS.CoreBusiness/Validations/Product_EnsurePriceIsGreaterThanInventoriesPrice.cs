using System.ComponentModel.DataAnnotations;

namespace IMS.CoreBusiness.Validations;

internal class Product_EnsurePriceIsGreaterThanInventoriesPrice : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (validationContext.ObjectInstance is Product product)
        {
            bool isPricingValid = product.ValidatePricing();
            if (isPricingValid == false)
            {
                var memberNames = new string[] { validationContext.MemberName };
                return new ValidationResult(
                    $"The product's price is less than the summary of its inventories' price: {product.TotalInventoryCost}",
                    memberNames);
            }
        }

        return ValidationResult.Success;
    }
}
