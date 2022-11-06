using IMS.CoreBusiness;

namespace IMS.UseCases;

public interface IViewProductsByNameUseCase
{
    Task<IReadOnlyList<Product>> ExecuteAsync(string name = "");
}