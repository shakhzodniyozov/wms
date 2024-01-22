using System.Text.Json.Serialization;
using AutoMapper;
using MediatR;
using WMS.Domain;

namespace WMS.Application;

public class CreateModelCommand : IRequest<ModelDto>
{
    public string Name { get; set; } = null!;
    public int YearOfIssue { get; set; }

    [JsonConverter(typeof(BodyTypeConverter))]
    public BodyTypes BodyType { get; set; }
    public Guid ManufacturerId { get; set; }
}

public class CreateModelCommandHandler : IRequestHandler<CreateModelCommand, ModelDto>
{
    private readonly IUnitOfWork uow;
    private readonly IManufacturerRepository manufacturerRepo;
    private readonly IMapper mapper;

    public CreateModelCommandHandler(IUnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        manufacturerRepo = uow.ManufacturerRepository;
        this.mapper = mapper;
    }

    public async Task<ModelDto> Handle(CreateModelCommand request, CancellationToken cancellationToken)
    {
        var manufacturer = await manufacturerRepo.GetByIdAsync(request.ManufacturerId);

        if (manufacturer == null)
            throw new Exception("Manufacturer with provided Id was not found.");

        var model = mapper.Map<Model>(request);
        manufacturer.Models.Add(model);

        await uow.SaveChangesAsync();

        return mapper.Map<ModelDto>(model);
    }
}