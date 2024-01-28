using MediatR;
using WMS.Domain;

namespace WMS.Application;

public class CreateSupplyOfGoodsCommand : IRequest
{
    public DateOnly Date { get; set; }
    public Dictionary<Guid, int> Products { get; set; } = null!;
}

public class CreateSupplyOfGoodsCommandHandler : IRequestHandler<CreateSupplyOfGoodsCommand, Unit>
{
    public CreateSupplyOfGoodsCommandHandler(IUnitOfWork uow)
    {
        this.uow = uow;
        supplyRepository = uow.SupplyRepository;
    }

    private readonly IUnitOfWork uow;
    private readonly ISupplyRepository supplyRepository;

    public async Task<Unit> Handle(CreateSupplyOfGoodsCommand request, CancellationToken cancellationToken)
    {
        SupplyOfGoods supply = new() { Id = Guid.NewGuid(), Date = request.Date };

        var products = await uow.ProductRepository.GetAllAsync(x => request.Products.Keys.Contains(x.Id));

        foreach (var product in products)
        {
            product.Quantity += request.Products[product.Id];

            supply.SupplyOfGoodsDetails.Add(new()
            {
                ProductId = product.Id,
                SupplyOfGoodsId = supply.Id,
                Quantity = request.Products[product.Id]
            });
        }

        await supplyRepository.CreateAsync(supply);
        uow.ProductRepository.UpdateRange(products.ToArray());

        await uow.SaveChangesAsync();

        return Unit.Value;
    }
}