using AutoMapper;
using IMS.CoreBusiness;

namespace IMS.UseCases.Mapping;

public class ProductTransactionSearchTypeConverter : ITypeConverter<ProductTransactionSearchType, ProductTransactionType?>
{
    public ProductTransactionType? Convert(ProductTransactionSearchType source, ProductTransactionType? destination, ResolutionContext context)
    {
        return source switch {
            ProductTransactionSearchType.AllActivities => null,
            ProductTransactionSearchType.ProduceProduct => ProductTransactionType.ProduceProduct,
            ProductTransactionSearchType.SellProduct => ProductTransactionType.SellProduct,
            _ => throw new InvalidCastException(),
        };
    }
}
