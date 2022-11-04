using IMS.CoreBusiness;

namespace IMS.UseCases;

public interface IViewInventoriesByNameUseCase
{
    Task<IReadOnlyList<Inventory>> ExecuteAsync(string name = "");
}