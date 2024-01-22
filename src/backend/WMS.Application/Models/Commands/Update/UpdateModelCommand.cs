using System.Text.Json.Serialization;
using AutoMapper;
using MediatR;
using WMS.Domain;

namespace WMS.Application;

public class UpdateModelCommand : IRequest<ModelDto>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int YearOfIssue { get; set; }

    [JsonConverter(typeof(BodyTypeConverter))]
    public BodyTypes BodyType { get; set; }
    public Guid ManufacturerId { get; set; }
}

public class UpdateModelCommandHandler : IRequestHandler<UpdateModelCommand, ModelDto>
{
    private readonly IUnitOfWork uow;
    private readonly IModelRepository modelRepo;
    private readonly IMapper mapper;

    public UpdateModelCommandHandler(IUnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        modelRepo = uow.ModelRepository;
        this.mapper = mapper;
    }

    public async Task<ModelDto> Handle(UpdateModelCommand request, CancellationToken cancellationToken)
    {
        var model = await modelRepo.GetByIdAsync(request.Id);

        if (model == null)
            throw new Exception("Model with provided Id was not found.");

        mapper.Map(request, model);
        modelRepo.Update(model);

        await uow.SaveChangesAsync();

        return mapper.Map<ModelDto>(model);
    }
}