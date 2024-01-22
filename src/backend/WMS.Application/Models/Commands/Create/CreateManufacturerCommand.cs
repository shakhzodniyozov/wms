using AutoMapper;
using MediatR;
using WMS.Domain;

namespace WMS.Application;

public class CreateManufacturerCommand : IRequest<ManufacturerDto>
{
    public string Name { get; set; } = null!;
}

public class CreateManufacturerCommandHandler : IRequestHandler<CreateManufacturerCommand, ManufacturerDto>
{
    private readonly IUnitOfWork uow;
    private readonly IManufacturerRepository manufacturerRepo;
    private readonly IMapper mapper;

    public CreateManufacturerCommandHandler(IUnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        manufacturerRepo = uow.ManufacturerRepository;
        this.mapper = mapper;
    }

    public async Task<ManufacturerDto> Handle(CreateManufacturerCommand request, CancellationToken cancellationToken)
    {
        Manufacturer manufacturer = await manufacturerRepo.CreateAsync(mapper.Map<Manufacturer>(request));

        await uow.SaveChangesAsync();
        return mapper.Map<ManufacturerDto>(manufacturer);
    }
}