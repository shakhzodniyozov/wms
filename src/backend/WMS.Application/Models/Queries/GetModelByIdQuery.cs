using AutoMapper;
using MediatR;
using WMS.Domain;

namespace WMS.Application;

public class GetModelByIdQuery : IRequest<ModelDto>
{
    public GetModelByIdQuery(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class GetModelByIdQueryHandler : IRequestHandler<GetModelByIdQuery, ModelDto>
{
    public GetModelByIdQueryHandler(IMapper mapper, IUnitOfWork uow)
    {
        this.mapper = mapper;
        this.uow = uow;
        modelRepository = uow.ModelRepository;
    }

    private readonly IMapper mapper;
    private readonly IUnitOfWork uow;
    private readonly IModelRepository modelRepository;

    public async Task<ModelDto> Handle(GetModelByIdQuery request, CancellationToken cancellationToken)
    {
        Model? model = await modelRepository.GetByIdAsync(request.Id);

        if (model == null)
            throw new Exception("Model with provided Id was not found.");

        return mapper.Map<ModelDto>(model);
    }
}
