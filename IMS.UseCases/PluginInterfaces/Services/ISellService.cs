using IMS.CoreBusiness;

namespace IMS.UseCases.PluginInterfaces;

public interface ISellService
{
    Task SellProduct(string salesOrderNumber, Product product, int quantity, string doneBy);
}
