using MediatR;
using WMS.Domain;

namespace WMS.Application;

public class CreateEngineCommand : IRequest
{
    public double Capacity { get; set; }
    public string FuelType { get; set; } = null!;
}

public class CreateEngineCommandHandler : IRequestHandler<CreateEngineCommand, Unit>
{
    public CreateEngineCommandHandler(IUnitOfWork uow)
    {
        this.uow = uow;
    }

    private readonly IUnitOfWork uow;

    public async Task<Unit> Handle(CreateEngineCommand request, CancellationToken cancellationToken)
    {
        var engine = new Engine()
        {
            FuelType = Enum.Parse<FuelType>(request.FuelType, true),
            Capacity = request.Capacity
        };

        await uow.EngineRepository.CreateAsync(engine);
        await uow.SaveChangesAsync();

        return Unit.Value;
    }
}
