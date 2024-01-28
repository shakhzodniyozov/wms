using MediatR;
using WMS.Domain;

namespace WMS.Application;

public class GetAllSuppliesQuery : IRequest<IEnumerable<SupplyOfGoodsDto>>
{

}

public class GetAllSuppliesQueryHandler : IRequestHandler<GetAllSuppliesQuery, IEnumerable<SupplyOfGoodsDto>>
{
    public GetAllSuppliesQueryHandler(IUnitOfWork uow)
    {
        this.uow = uow;
        supplyRepository = uow.SupplyRepository;
    }
    private readonly IUnitOfWork uow;
    private readonly ISupplyRepository supplyRepository;

    public async Task<IEnumerable<SupplyOfGoodsDto>> Handle(GetAllSuppliesQuery request, CancellationToken cancellationToken)
    {
        var supplies = await supplyRepository.GetAllAsync(disableTracking: true);

        return supplies.Select(s => new SupplyOfGoodsDto() { Id = s.Id, Date = s.Date });
    }
}