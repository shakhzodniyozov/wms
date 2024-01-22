using AutoMapper;
using MediatR;
using WMS.Domain;

namespace WMS.Application;

public class DeleteManufacturerCommand : IRequest
{
    public DeleteManufacturerCommand(Guid id) => Id = id;

    public Guid Id { get; set; }
}

public class DeleteManufacturerCommandHandler : IRequestHandler<DeleteManufacturerCommand>
{
    public DeleteManufacturerCommandHandler(IUnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        manufacturerRepo = uow.ManufacturerRepository;
        this.mapper = mapper;
    }

    private readonly IUnitOfWork uow;
    private readonly IManufacturerRepository manufacturerRepo;
    private readonly IMapper mapper;

    public async Task<Unit> Handle(DeleteManufacturerCommand request, CancellationToken cancellationToken)
    {
        Manufacturer? manufacturer = await manufacturerRepo.GetByIdAsync(request.Id);

        if (manufacturer == null)
            throw new Exception("Manufacturer with provided Id was not found");

        manufacturerRepo.Delete(manufacturer);
        await uow.SaveChangesAsync();

        return Unit.Value;
    }
}