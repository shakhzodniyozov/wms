using AutoMapper;
using MediatR;

namespace WMS.Application;

public class GetAllEnginesQuery : IRequest<IEnumerable<EngineDto>>
{

}

public class GetAllEnginesQueryHandler : IRequestHandler<GetAllEnginesQuery, IEnumerable<EngineDto>>
{
    public GetAllEnginesQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        this.mapper = mapper;
    }

    private readonly IUnitOfWork uow;
    private readonly IMapper mapper;

    public async Task<IEnumerable<EngineDto>> Handle(GetAllEnginesQuery request, CancellationToken cancellationToken)
    {
        var engines = await uow.EngineRepository.GetAllAsync(disableTracking: true);

        return mapper.Map<IEnumerable<EngineDto>>(engines);
    }
}