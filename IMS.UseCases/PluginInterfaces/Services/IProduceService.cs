using IMS.CoreBusiness;

namespace IMS.UseCases.PluginInterfaces;

public interface IProduceService
{
    Task ProduceProduct(string productionNumber, Product product, int quantity, string doneBy);
}
