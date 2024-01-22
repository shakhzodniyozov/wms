using AutoMapper;
using MediatR;

namespace WMS.Application;

public class GetProductByIdQuery : IRequest<ProductDetailsDto>
{
    public GetProductByIdQuery(Guid productId) => ProductId = productId;

    public Guid ProductId { get; set; }
}


public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDetailsDto>
{
    private readonly IUnitOfWork uow;
    private readonly IMapper mapper;
    private readonly IProductRepository productRepository;

    public GetProductByIdQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        this.mapper = mapper;
        productRepository = uow.ProductRepository;
    }

    public async Task<ProductDetailsDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId, "Category,Image,Models,Prices");

        if (product == null)
            throw new Exception("Product with provided Id was not found");

        var productDetails = mapper.Map<ProductDetailsDto>(product);

        productDetails.ManufacturerId = product.Models.Count == 0 ? null : product.Models[0].ManufacturerId;
        productDetails.ForAllManufacturers = product.Models.Count == 0;
        productDetails.Price = product.Prices.OrderByDescending(x => x.DateTime).First().Value;

        productDetails.Models = product.Models.GroupBy(x => x.Name)
                                                .Select(group =>
                                                    {
                                                        var dto = new ModelWithYearsOfIssueDto
                                                        {
                                                            Model = group.Key
                                                        };
                                                        dto.YearsOfIssue.AddRange(group.Select(model => model.YearOfIssue));
                                                        return dto;
                                                    }).ToArray();

        return productDetails;
    }
}