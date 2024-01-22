using AutoMapper;
using WMS.Domain;

namespace WMS.Application;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductCommand, Product>()
            .ForMember(x => x.Image, opt => opt.Ignore())
            .ForMember(x => x.Models, opt => opt.Ignore());

        CreateMap<Product, ProductDetailsDto>()
            .ForMember(x => x.Image, opt => opt.MapFrom(x => x.Image.Name))
            .ForMember(x => x.Models, opt => opt.Ignore())
            .ForMember(x => x.Price, opt => opt.Ignore());
    }
}
