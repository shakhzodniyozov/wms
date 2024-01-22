using AutoMapper;
using MediatR;

namespace WMS.Application;

public class GetPreliminaryDataForNewProduct : IRequest<ManufacturersAndCategoriesDto>
{

}

public class GetPreliminaryDataForNewProductHandler : IRequestHandler<GetPreliminaryDataForNewProduct, ManufacturersAndCategoriesDto>
{
    private readonly IUnitOfWork uow;
    private readonly IMapper mapper;

    public GetPreliminaryDataForNewProductHandler(IUnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        this.mapper = mapper;
    }

    public async Task<ManufacturersAndCategoriesDto> Handle(GetPreliminaryDataForNewProduct request, CancellationToken cancellationToken)
    {
        var manufacturers = mapper.Map<List<ManufacturerDto>>(await uow.ManufacturerRepository.GetAllAsync(disableTracking: true));
        var categories = mapper.Map<List<CategoryDto>>(await uow.CategoryRepository.GetAllAsync(disableTracking: true));

        return new ManufacturersAndCategoriesDto() { Manufacturers = manufacturers, Categories = categories };
    }
}