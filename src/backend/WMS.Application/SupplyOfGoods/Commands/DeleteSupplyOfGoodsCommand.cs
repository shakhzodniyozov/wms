using MediatR;

namespace WMS.Application;

public class DeleteSupplyOfGoodsCommand : IRequest
{
    public DeleteSupplyOfGoodsCommand(Guid supplyId) => SupplyId = supplyId;

    public Guid SupplyId { get; }
}

public class DeleteSupplyOfGoodsCommandHandler : IRequestHandler<DeleteSupplyOfGoodsCommand, Unit>
{
    private readonly IUnitOfWork uow;
    private readonly ISupplyRepository supplyRepository;

    public DeleteSupplyOfGoodsCommandHandler(IUnitOfWork uow)
    {
        this.uow = uow;
        supplyRepository = uow.SupplyRepository;
    }

    public async Task<Unit> Handle(DeleteSupplyOfGoodsCommand request, CancellationToken cancellationToken)
    {
        var supply = await supplyRepository.GetByIdAsync(request.SupplyId);

        if (supply == null)
            throw new Exception("Supply Of Goods with provided Id was not found.");

        supplyRepository.Delete(supply);
        await uow.SaveChangesAsync();

        return Unit.Value;
    }
}