using AutoMapper;
using IMS.CoreBusiness;

namespace IMS.UseCases.Mapping;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<InventoryTransactionSearchType, InventoryTransactionType?>().ConvertUsing<InventoryTransactionSearchTypeConverter>();
    }
}
