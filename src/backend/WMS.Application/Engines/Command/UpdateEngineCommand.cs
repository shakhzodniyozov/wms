using MediatR;
using WMS.Domain;

namespace WMS.Application;

public class UpdateEngineCommand : CreateEngineCommand, IRequest
{
    public UpdateEngineCommand(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class UpdateEngineCommandHandler : IRequestHandler<UpdateEngineCommand, Unit>
{
    public UpdateEngineCommandHandler(IUnitOfWork uow)
    {
        this.uow = uow;
    }

    private readonly IUnitOfWork uow;

    public async Task<Unit> Handle(UpdateEngineCommand request, CancellationToken cancellationToken)
    {
        var engine = await uow.EngineRepository.GetByIdAsync(request.Id);

        if (engine == null)
            throw new Exception("Engine with provided Id was not found.");

        engine.Capacity = request.Capacity;
        engine.FuelType = Enum.Parse<FuelType>(request.FuelType);

        uow.EngineRepository.Update(engine);
        await uow.SaveChangesAsync();

        return Unit.Value;
    }
}