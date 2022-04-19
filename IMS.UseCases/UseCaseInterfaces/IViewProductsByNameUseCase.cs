using IMS.CoreBusiness;

namespace IMS.UseCases;

public interface IViewProductsByNameUseCase
{
    Task<IEnumerable<Product>> ExecuteAsync(string name = "");
}