using IMS.CoreBusiness;

namespace IMS.UseCases;

public interface IProduceProductUseCase
{
    Task ExecuteAync(string productionNumber, Product product, int quantity, double price, string doneBy);
}