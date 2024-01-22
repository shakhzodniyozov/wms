using AutoMapper;
using MediatR;

namespace WMS.Application;

public class GetProductsQuery : IRequest<IEnumerable<ProductDto>>
{
    public GetProductsQuery(int page = 1, int pageSize = 50)
    {
        Page = page;
        PageSize = pageSize;
    }

    public int Page { get; }
    public int PageSize { get; }
}

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IUnitOfWork uow;
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;

    public GetProductsQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        productRepository = uow.ProductRepository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = (await productRepository.GetAllAsync()).Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).OrderBy(x => x.Name);

        return mapper.Map<IEnumerable<ProductDto>>(products);
    }
}