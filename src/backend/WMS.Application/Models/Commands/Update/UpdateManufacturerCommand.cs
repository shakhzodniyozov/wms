using AutoMapper;
using MediatR;
using WMS.Domain;

namespace WMS.Application;

public class UpdateManufacturerCommand : IRequest<ManufacturerDto>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}

public class UpdateManufacturerCommandHandler : IRequestHandler<UpdateManufacturerCommand, ManufacturerDto>
{
    public UpdateManufacturerCommandHandler(IUnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        manufacturerRepo = uow.ManufacturerRepository;
        this.mapper = mapper;
    }

    private readonly IUnitOfWork uow;
    private readonly IManufacturerRepository manufacturerRepo;
    private readonly IMapper mapper;

    public async Task<ManufacturerDto> Handle(UpdateManufacturerCommand request, CancellationToken cancellationToken)
    {
        Manufacturer? manufacturer = await manufacturerRepo.GetByIdAsync(request.Id);

        if (manufacturer == null)
            throw new Exception("Manufacturer with provided Id was not found");

        mapper.Map(request, manufacturer);
        await uow.SaveChangesAsync();

        return mapper.Map<ManufacturerDto>(manufacturer);
    }
}
