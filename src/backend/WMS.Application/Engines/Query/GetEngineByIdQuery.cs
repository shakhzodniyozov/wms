using AutoMapper;
using MediatR;

namespace WMS.Application;

public class GetEngineByIdQuery : IRequest<EngineDto>
{
    public GetEngineByIdQuery(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class GetEngineByIdQueryHandler : IRequestHandler<GetEngineByIdQuery, EngineDto>
{
    public GetEngineByIdQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        this.mapper = mapper;
    }

    private readonly IUnitOfWork uow;
    private readonly IMapper mapper;

    public async Task<EngineDto> Handle(GetEngineByIdQuery request, CancellationToken cancellationToken)
    {
        var engine = await uow.EngineRepository.GetByIdAsync(request.Id, disableTracking: true);

        if (engine == null)
            throw new Exception("Engine with provided Id was not found.");

        return mapper.Map<EngineDto>(engine);
    }
}
