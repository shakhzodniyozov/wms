using MediatR;

namespace WMS.Application;

public class GetProductsInCellQuery : IRequest<IEnumerable<ProductDto>>
{
    public GetProductsInCellQuery(Guid addressId) => AddressId = addressId;
    public Guid AddressId { get; set; }
}

public class GetProductsInCellQueryHandler : IRequestHandler<GetProductsInCellQuery, IEnumerable<ProductDto>>
{
    public GetProductsInCellQueryHandler(IUnitOfWork uow)
    {
        this.uow = uow;
    }
    private readonly IUnitOfWork uow;

    public async Task<IEnumerable<ProductDto>> Handle(GetProductsInCellQuery request, CancellationToken cancellationToken)
    {
        var address = await uow.AddressRepository.GetByIdIncludingProductsAsync(request.AddressId, true);

        if (address == null)
            throw new Exception("Address with provided Id was not found.");
        var dtos = new List<ProductDto>();
        address.ProductAddresses.ForEach(x =>
        {
            dtos.Add(new()
            {
                Id = x.ProductId,
                Code = x.Product.Code,
                Description = x.Product.Description,
                Name = x.Product.Name,
                Quantity = x.Quantity
            });
        });

        return dtos;
    }
}