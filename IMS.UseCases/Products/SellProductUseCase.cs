using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases;

public sealed class SellProductUseCase : ISellProductUseCase
{
    private readonly ISellService sellService;

    public SellProductUseCase(ISellService sellService)
    {
        this.sellService = sellService;
    }

    public async Task ExecuteAsync(string salesOrderNumber, Product product, int quantity, string doneBy)
    {
        await sellService.SellProduct(salesOrderNumber, product, quantity, doneBy);
    }
}
