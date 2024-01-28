using MediatR;

namespace WMS.Application;

public class UpdateSupplyCommand : CreateSupplyOfGoodsCommand, IRequest
{
    public UpdateSupplyCommand(Guid supplyId) => SupplyId = supplyId;
    public Guid SupplyId { get; set; }
}

public class UpdateSupplyCommandHandler : IRequestHandler<UpdateSupplyCommand, Unit>
{
    private readonly IUnitOfWork uow;
    private readonly ISupplyRepository supplyRepository;
    private readonly IProductRepository productRepository;

    public UpdateSupplyCommandHandler(IUnitOfWork uow)
    {
        this.uow = uow;
        supplyRepository = uow.SupplyRepository;
        productRepository = uow.ProductRepository;
    }

    public async Task<Unit> Handle(UpdateSupplyCommand request, CancellationToken cancellationToken)
    {
        var supply = await supplyRepository.GetSupplyIncludingProducts(request.SupplyId);

        if (supply == null)
            throw new Exception("Supply Of Goods with provided Id was not found.");

        var productIdsGonnaBeDelete = supply.SupplyOfGoodsDetails.Select(x => x.ProductId).Except(request.Products.Keys).ToList();
        var productIdsGonnaBeAdd = request.Products.Keys.Except(supply.SupplyOfGoodsDetails.Select(x => x.ProductId)).ToList();
        var intersect = supply.SupplyOfGoodsDetails.IntersectBy(request.Products.Keys, x => x.ProductId).ToList();

        productIdsGonnaBeAdd.ForEach(productId =>
        {
            supply.SupplyOfGoodsDetails.Add(new()
            {
                ProductId = productId,
                Quantity = request.Products[productId]
            });
        });

        supply.SupplyOfGoodsDetails = supply.SupplyOfGoodsDetails.Where(x => !productIdsGonnaBeDelete.Contains(x.ProductId)).ToList();

        foreach (var supDetail in intersect)
        {
            if (supDetail.Quantity != request.Products[supDetail.ProductId])
                supDetail.Quantity = request.Products[supDetail.ProductId];
        }

        if (supply.Date != request.Date)
            supply.Date = request.Date;

        supplyRepository.Update(supply);
        await uow.SaveChangesAsync();

        return Unit.Value;
    }
}