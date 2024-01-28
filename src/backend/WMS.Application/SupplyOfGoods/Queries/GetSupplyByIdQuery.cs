using MediatR;

namespace WMS.Application;

public class GetSupplyByIdQuery : IRequest<SupplyOfGoodsDto>
{
    public GetSupplyByIdQuery(Guid id) => SupplyId = id;
    public Guid SupplyId { get; set; }
}

public class GetSupplyByIdQueryHandler : IRequestHandler<GetSupplyByIdQuery, SupplyOfGoodsDto>
{
    public GetSupplyByIdQueryHandler(IUnitOfWork uow)
    {
        this.uow = uow;
    }

    private readonly IUnitOfWork uow;

    public async Task<SupplyOfGoodsDto> Handle(GetSupplyByIdQuery request, CancellationToken cancellationToken)
    {
        var supply = await uow.SupplyRepository.GetSupplyIncludingProducts(request.SupplyId);

        if (supply == null)
            throw new Exception("Supply Of Goods with provided Id was not found.");

        var dto = new SupplyOfGoodsDto()
        {
            Date = supply.Date,
            Id = supply.Id
        };

        foreach (var sog in supply.SupplyOfGoodsDetails)
        {
            string productName = "";
            if (sog.Product.Models.Count == 0)
                productName = $"{sog.Product.Name} / For all manufacturers";
            else
            {
                if (sog.Product.Models.Count == sog.Product.Models.First()!.Manufacturer.Models.Count)
                    productName = $"{sog.Product.Name} / For all models";
                else
                {
                    foreach (var model in sog.Product.Models)
                    {
                        dto.SupplyDetails.Add(new()
                        {
                            Product = $"{sog.Product.Name} /{model.Manufacturer.Name}/{model.Name}/{model.YearOfIssue}",
                            ProductId = sog.ProductId,
                            Quantity = sog.Quantity
                        });
                    }
                    continue;
                }
            }

            dto.SupplyDetails.Add(new()
            {
                Product = productName,
                ProductId = sog.ProductId,
                Quantity = sog.Quantity
            });
        }

        return dto;
    }
}
