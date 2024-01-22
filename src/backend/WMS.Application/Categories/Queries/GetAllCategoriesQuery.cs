using AutoMapper;
using MediatR;

namespace WMS.Application;

public class GetAllCategoriesQuery : IRequest<List<CategoryDto>>
{

}

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryDto>>
{
    public GetAllCategoriesQueryHandler(IMapper mapper, IUnitOfWork uow)
    {
        this.mapper = mapper;
        this.uow = uow;
        categoryRepo = uow.CategoryRepository;
    }

    private readonly IMapper mapper;
    private readonly IUnitOfWork uow;
    private readonly ICategoryRepository categoryRepo;

    public async Task<List<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepo.GetAllAsync(disableTracking: true);

        return mapper.Map<List<CategoryDto>>(categories.OrderBy(x => x.Name));
    }
}