using AutoMapper;
using MediatR;

namespace WMS.Application;

public class GetManufacturersQuery : IRequest<List<ManufacturerDto>> { }

public class GetManufacturersQueryHandler : IRequestHandler<GetManufacturersQuery, List<ManufacturerDto>>
{
    public GetManufacturersQueryHandler(IMapper mapper, IUnitOfWork uow)
    {
        this.mapper = mapper;
        this.uow = uow;
        manufacturerRepo = uow.ManufacturerRepository;
    }

    private readonly IMapper mapper;
    private readonly IUnitOfWork uow;
    private readonly IManufacturerRepository manufacturerRepo;

    public async Task<List<ManufacturerDto>> Handle(GetManufacturersQuery request, CancellationToken cancellationToken)
    {
        var manufactors = await manufacturerRepo.GetAllAsync(disableTracking: true);

        return mapper.Map<List<ManufacturerDto>>(manufactors);
    }
}