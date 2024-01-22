using AutoMapper;
using WMS.Domain;

namespace WMS.Application;

public class ManufactorModelProfile : Profile
{
    public ManufactorModelProfile()
    {
        CreateMap<CreateManufacturerCommand, Manufacturer>();

        CreateMap<Manufacturer, ManufacturerDto>();
        CreateMap<UpdateManufacturerCommand, Manufacturer>();
        CreateMap<CreateModelCommand, Model>();
        CreateMap<Model, ModelDto>();
        CreateMap<CreateProductCommand, Product>();
        CreateMap<Product, ProductDto>();
    }
}
