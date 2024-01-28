using MediatR;

namespace WMS.Application;

public class GetSuggestionQuery : IRequest<IEnumerable<ProductForAutocompleteDto>>
{
    public GetSuggestionQuery(string productName) => ProductName = productName;

    public string ProductName { get; set; }
}

public class GetSuggestionQueryHandler : IRequestHandler<GetSuggestionQuery, IEnumerable<ProductForAutocompleteDto>>
{
    public GetSuggestionQueryHandler(IUnitOfWork uow)
    {
        this.uow = uow;
        productRepository = uow.ProductRepository;
    }

    private readonly IUnitOfWork uow;
    private readonly IProductRepository productRepository;

    public async Task<IEnumerable<ProductForAutocompleteDto>> Handle(GetSuggestionQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetProductsIncludingModels(x => x.Name.ToUpper().Contains(request.ProductName.ToUpper()));
        List<ProductForAutocompleteDto> dtos = new();

        products.ForEach(product =>
        {
            if (product.Models.Count == 0)
                dtos.Add(new() { Id = product.Id, Name = $"{product.Name} / For all manufacturers" });
            else
            {
                if (product.Models.Count == product.Models.First()!.Manufacturer.Models.Count)
                {
                    dtos.Add(new() { Id = product.Id, Name = $"{product.Name} /{product.Models[0].Manufacturer.Name}/ For all models" });
                }
                foreach (var model in product.Models)
                {
                    dtos.Add(new() { Id = product.Id, Name = $"{product.Name} /{model.Manufacturer.Name}/{model.Name}/{model.YearOfIssue}" });
                }
            }
        });

        return dtos;
    }
}