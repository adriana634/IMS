using System.ComponentModel.DataAnnotations;

namespace IMS.CoreBusiness.Validations
{
    internal class Product_EnsurePriceIsGreaterThanInventoriesPrice : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            Product? product = validationContext.ObjectInstance as Product;
            if (product != null)
            {
                bool isPricingValid = product.ValidatePricing();
                if (!isPricingValid)
                    return new ValidationResult("The product's price is less than the summary of its inventories' price!", new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }
    }
}
