using AutoMapper;
using MediatR;

namespace WMS.Application;

public class GetAddressesQuery : IRequest<IEnumerable<AddressDto>>
{

}

public class GetAddressesQueryHandler : IRequestHandler<GetAddressesQuery, IEnumerable<AddressDto>>
{
    private readonly IUnitOfWork uow;
    private readonly IMapper mapper;

    public GetAddressesQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<AddressDto>> Handle(GetAddressesQuery request, CancellationToken cancellationToken)
    {
        var addresses = await uow.AddressRepository.GetAllAsync(disableTracking: true);
        addresses = addresses.OrderBy(x => x.Line)
                             .ThenBy(x => x.Section)
                             .ThenBy(x => x.Level)
                             .ThenBy(x => x.Cell)
                             .ToList();

        return mapper.Map<AddressDto[]>(addresses);
    }
}