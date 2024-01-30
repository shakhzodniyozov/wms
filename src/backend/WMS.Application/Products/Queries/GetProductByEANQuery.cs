using MediatR;

namespace WMS.Application;

public class GetProductByEANQuery : IRequest<object>
{
    public GetProductByEANQuery(string ean) => EAN = ean;
    public string EAN { get; set; } = null!;
}

public class GetProductByEANQueryHandler : IRequestHandler<GetProductByEANQuery, object>
{
    public GetProductByEANQueryHandler(IUnitOfWork uow)
    {
        this.uow = uow;
        productRepository = uow.ProductRepository;
    }

    private readonly IUnitOfWork uow;
    private readonly IProductRepository productRepository;

    public async Task<object> Handle(GetProductByEANQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByEAN(request.EAN);

        if (product == null)
            throw new Exception("Product with provided EAN was not found.");

        return new { Id = product.Id, Name = product.Name };
    }
}