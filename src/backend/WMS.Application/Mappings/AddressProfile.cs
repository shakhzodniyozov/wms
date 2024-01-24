using AutoMapper;
using WMS.Domain;

namespace WMS.Application;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<Address, AddressDto>();
    }
}
